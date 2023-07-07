using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ThuongMaiDienTu.Models;
using PagedList;

namespace ThuongMaiDienTu.Controllers
{
    
    public class HomeController : Controller
    {
        ThuongMaiDienTuEntities db = new ThuongMaiDienTuEntities();
        // GET: Home
        public ActionResult TrangChu()
        {
              
            ViewBag.LoaiSP = db.LoaiSanPhams.ToList();
            List<SanPham> List_sp = db.SanPhams.ToList();
            Session["lsp"] = db.LoaiSanPhams.ToList();            
            return View(List_sp);
        }
        public ActionResult TatCaSanPham(int? page, int? pageSize)
        {
            if (page == null)
            {
                page = 1;
            }
            if (pageSize == null)
            {
                pageSize = 12;
            }
            List<SanPham> List_sp = db.SanPhams.Where(s=>s.Xoa.Value == false).ToList();
            return View(List_sp.ToPagedList((int)page, (int)pageSize));
        }
        public ActionResult TimKiemTheoLoai(string MaLoai)
        {
            ViewBag.LoaiSP = db.LoaiSanPhams.ToList();           
            List<SanPham> List_sp = new List<SanPham>();
            List<ChiTietLoaiSanPham> ct = db.ChiTietLoaiSanPhams.Where(t => t.MaLoai.Equals(MaLoai)).ToList();
            foreach (ChiTietLoaiSanPham item in ct)
                List_sp.AddRange(item.SanPhams.Where(s=>s.Xoa.Value == false).ToList());
            ViewBag.LoaiSP = db.LoaiSanPhams.ToList();
            return View(List_sp);
            
        }
        public ActionResult Layout()
        {
            Session["lsp"] = db.LoaiSanPhams.ToList();
            return View();
        }
        public ActionResult hi()
        {
            return View();
        }
        public ActionResult hihi()
        {
            return View();
        }
        public ActionResult DangNhap(string url)
        {
           Session["url"] = url;
            return View();
        }
        public ActionResult DangKy()
        {
            return View();
        }
        public ActionResult TimKiem(string txt_search)
        {
            if (txt_search == null || txt_search.Trim() == "")
                return View(db.SanPhams.Where(s=>s.Xoa==false).ToList());
            return View(db.SanPhams.Where(s =>s.Xoa == false && s.TenSP.Contains(txt_search)).ToList());
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection f)
        {
            try
            {
                TaiKhoan Tk = new TaiKhoan();
                Tk.TenTaiKhoan = HoTro.ToFirstUpper(f["TenTaiKhoan"].Trim());
                if (db.TaiKhoans.Count(t => t.TenTaiKhoan.Equals(Tk.TenTaiKhoan)) > 0)
                {
                    ViewData["TenTaiKhoan"] = "Tên tài khoản đã tồn tại, hãy tạo 1 tên khác!!!";
                    return View();
                }
                Tk.MatKhau = HoTro.HashToSHA512(f["MK"]);
                NguoiDung nd = new NguoiDung();
                nd.ID = "ND00" + (1 + db.NguoiDungs.Count()).ToString();
                nd.TenNguoiDung = f["HoVaTen"];
                nd.SoDienThoai = f["SoDienThoai"];
                nd.Email = f["Email"];
                Tk.NguoiDung = nd;
                Tk.ID = nd.ID;
                db.NguoiDungs.Add(nd);
                db.TaiKhoans.Add(Tk);
                db.SaveChanges();
                Session["TaiKhoan"] = Tk;
            }
            catch
            {
                ViewBag.loidk = true;
                return View();
            }
            return RedirectToAction("TrangChu");
        }
        [HttpPost]
        public ActionResult DangNhap(string TenTK,string MatKhau)
        {
                TaiKhoan tk = db.TaiKhoans.FirstOrDefault(t => t.TenTaiKhoan.Equals(TenTK));
                if (tk != null)
                    if (tk.MatKhau.Equals(HoTro.HashToSHA512(MatKhau)))
                    {
                        Session["TaiKhoan"] = tk;
                        if (Session["giohang"] != null)
                        {
                            List<ChiTietGioHang> GHs = Session["giohang"] as List<ChiTietGioHang>;
                            GHs.ForEach(item => db.ThemSPVaoGioHang(tk.ID, item.MaCTKichCo, item.SoLuong));
                            db.SaveChanges();
                            GHs = null;
                            try
                            {
                                Session["slSp_ghang"] = db.ChiTietGioHangs.Where(g => g.ID.Equals(tk.ID)).Sum(s => s.SoLuong);                                
                            }
                            catch
                            {
                                Session["slSp_ghang"] = 0;
                            }
                        Session.Remove("giohang");
                    }
                    if (Session["url"] != null)
                        return Redirect(Session["url"].ToString());
                    else
                        return RedirectToAction("TrangChu");
                        
                    }        
            
            ViewData["tk"] = "Tài khoản hoặc mật khẩu không đúng, vui lòng kiểm tra lại!";
            return View();
        }
        public ActionResult DangXuat(string url)
        {
            try
            {
                Session.Remove("slSp_ghang");
                Session.Remove("TaiKhoan");
                return Redirect(url);
            }
            catch
            {
                return RedirectToAction("TrangChu", "Home");
            }

        }
        public ActionResult TimKiemTheoChiTietLoai(string MaCT)
        {
            IEnumerable<SanPham> SPs = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44304/api/");
                var responseTask = client.GetAsync("SanPhamApi/GetListLoaiCTSanPham?MaCT=" + MaCT);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readSP = result.Content.ReadAsAsync<IList<SanPham>>();
                    readSP.Wait();
                    SPs = readSP.Result;
                }
                else
                {
                    SPs = Enumerable.Empty<SanPham>();
                    ModelState.AddModelError(string.Empty, "Erorr occured !!!");
                }
                ViewBag.loai = db.ChiTietLoaiSanPhams.Single(ct => ct.MaChiTietLoai.Equals(MaCT)).TenChiTiet;
                return View(SPs);
            }
        }
    }
}