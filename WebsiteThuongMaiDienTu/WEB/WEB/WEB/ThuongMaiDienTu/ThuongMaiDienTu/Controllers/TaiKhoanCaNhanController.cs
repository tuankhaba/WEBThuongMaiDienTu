using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Controllers
{
    public class TaiKhoanCaNhanController : Controller
    {
        ThuongMaiDienTuEntities db = new ThuongMaiDienTuEntities();
        
        // GET: TaiKhoanCaNhan
        public ActionResult TrangCaNhan()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ThongTinCaNhan()
        {
            TaiKhoan TK = Session["TaiKhoan"] as TaiKhoan;
            try
            {
                return View(db.NguoiDungs.Include("TaiKhoans").Single(t => t.ID == TK.ID));
            }
            catch
            {
                return RedirectToAction("TrangChu", "Home");
            }
        }
        public ActionResult SoDiaChi()
        {
            TaiKhoan TK = Session["TaiKhoan"] as TaiKhoan;
            if (TK != null)
            {
                List<ThongTinLienHe> TTs = db.ThongTinLienHes.Include("Xa_Phuong").Where(t => t.ID == TK.ID).ToList();
                return View(TTs);
            }
            else
                return RedirectToAction("TrangChu", "Home");
        }
        public ActionResult ThemDiaChiMoi()
        {
            ViewBag.loi = -1;
            ViewBag.Tinh = db.Tinh_ThanhPho.ToList();
            ViewBag.Quan = db.Quan_Huyen.ToList();
            ViewBag.Xa = db.Xa_Phuong.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult ThemDiaChiMoi(FormCollection f, string url)
        {
            
            TaiKhoan TK = Session["TaiKhoan"] as TaiKhoan;
            if (TK != null)
            {
                if (ModelState.IsValid)
                {
                    string HoVaTen = f["HoVaTen"];
                    string SoDienThoai = f["SoDienThoai"];
                    string MaXP = f["XaPhuong"];
                    ViewBag.Tinh = db.Tinh_ThanhPho.ToList();
                    ViewBag.Quan = db.Quan_Huyen.ToList();
                    ViewBag.Xa = db.Xa_Phuong.ToList();
                    if (HoVaTen == null || SoDienThoai == null || MaXP == null)
                        return View();
                    string LoaiDiaChi = f["LoaiDiaChi"];
                    bool MacDinh = (f["MacDinh"] == null) ? false : true;
                    ThongTinLienHe lh = new ThongTinLienHe();

                    lh.NguoiDung = db.NguoiDungs.Single(nd => nd.ID == TK.ID);
                    lh.ID = TK.ID;
                    lh.LoaiDiaChi = LoaiDiaChi;
                    lh.MacDinh = MacDinh;
                    lh.MaXP = MaXP;
                    lh.DiaChiCuThe = f["DiaChiCuThe"];
                    lh.SoDienThoai = SoDienThoai;
                    lh.TenNguoiNhan = HoVaTen;
                    lh.MaLH = lh.ID.ToString().Trim() + "LH" + db.ThongTinLienHes.Count(t => t.ID == lh.ID).ToString();
                    lh.An = false;

                    db.ThongTinLienHes.Add(lh);
                    db.SaveChanges();
                    ViewBag.checkDC = true;
                }
                return Redirect(url);
            }
            else
                return RedirectToAction("TrangChu", "Home");
        }
        [HttpGet]
        public ActionResult XoaDiaChi(string MaLH)
        {
            ThongTinLienHe tt = db.ThongTinLienHes.Single(t => t.MaLH.Equals(MaLH));
            tt.An = true;
            db.ThongTinLienHes.Attach(tt);
            db.Entry(tt).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("SoDiaChi");
        }
        public ActionResult SuaDiaChi(string MaLH, string url)
        {
            TaiKhoan TK = Session["TaiKhoan"] as TaiKhoan;
            if (TK != null)
            {
                ViewBag.Tinh = db.Tinh_ThanhPho.ToList();
                ViewBag.Quan = db.Quan_Huyen.ToList();
                ViewBag.Xa = db.Xa_Phuong.ToList();
                Session["url"] = url;
                return View(db.ThongTinLienHes.Single(t => t.MaLH.Equals(MaLH)));
            } 
            else
                return RedirectToAction("TrangChu", "Home");
        }
        [HttpPost]
        public ActionResult SuaDiaChi(FormCollection f)
        {
            TaiKhoan TK = Session["TaiKhoan"] as TaiKhoan;
            if (TK != null)
            {
                string malh = f["MaLH"];
                ThongTinLienHe ttlh = db.ThongTinLienHes.Single(t => t.MaLH.Equals(malh));
                ttlh.MaXP = f["XaPhuong"];
                ttlh.TenNguoiNhan = f["HoVaTen"];
                ttlh.SoDienThoai = f["SoDienThoai"];
                ttlh.MacDinh = (f["MacDinh"] == null) ? false : true;
                ttlh.LoaiDiaChi = f["LoaiDiaChi"];
                db.ThongTinLienHes.Attach(ttlh);
                db.Entry(ttlh).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Redirect(Session["url"].ToString());
            } 
            else
                return RedirectToAction("TrangChu", "Home");
        }
        [HttpGet]
        public ActionResult DatlamMacDinh(string MaLH)
        {
            
            ThongTinLienHe tt = db.ThongTinLienHes.Single(t => t.MaLH.Equals(MaLH));
            tt.MacDinh = true;
            db.ThongTinLienHes.Attach(tt);
            db.Entry(tt).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("SoDiaChi");
        }
        public ActionResult ThanhToan()
        {
            return View();
        }
        public ActionResult DoiMatKhau()
        {
            TaiKhoan TK = Session["TaiKhoan"] as TaiKhoan;
            if (TK != null)
            {
                return View();
            }
            else
                return RedirectToAction("TrangChu", "Home");
        }
        public ActionResult HuyDonHang(string MaDH, string url)
        {
            TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;
            if (tk != null)
            {
                DonHang DH = db.DonHangs.Single(d => d.MaDonHang.Equals(MaDH));
                DH.TrangThai = "Đã hủy";
                db.Entry(DH).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Redirect(url);
            }
            return RedirectToAction("TrangChu", "Home");
        }
        [HttpPost]
        public ActionResult DoiMatKhau(FormCollection f)
        {
            TaiKhoan TK = Session["TaiKhoan"] as TaiKhoan;
            if (TK.MatKhau.Equals(HoTro.HashToSHA512(f["MKHienTai"])))
            {
                string MK = HoTro.HashToSHA512(f["MKMoi"].ToString());
                db.ThayDoiMatKhau(TK.ID, MK);
                db.SaveChanges();
                return RedirectToAction("ThongTinCaNhan");
            }
            ViewData["MKHienTai"] = "Sai mật khẩu, vui lòng nhập lại!";
            return View();
        }
        public ActionResult XemChiTietDonHang(string maDH)
        {
            TaiKhoan TK = Session["TaiKhoan"] as TaiKhoan;
            if (TK != null)
            {
                DonHang Dh = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44304/api/");
                    var responseTask = client.GetAsync("DonHangApi/XemChiTietDonHang?maDH=" + maDH);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readSP = result.Content.ReadAsAsync<DonHang>();
                        readSP.Wait();
                        Dh = readSP.Result;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Erorr occured !!!");
                    }
                    return View(Dh);
                }
            }
            else
                return RedirectToAction("TrangChu", "Home");
            
        }
        public ActionResult YeuThich()
        {
            return View();
        }
        public ActionResult UuDaiCuaToi()
        {
            return View();
        }
        public ActionResult DoiTra()
        {
            return View();
        }
        public ActionResult QuanLyDonHang()
        {
            TaiKhoan TK = Session["TaiKhoan"] as TaiKhoan;
            if (TK != null)
            {
                List<DonHang> GHangs = db.DonHangs.Include("ThongTinLienHe").Where(gh => gh.ThongTinLienHe.ID == TK.ID).ToList();
                return View(GHangs);
            }
            else
                return RedirectToAction("TrangChu", "Home");
        }
        [HttpPost]
        public ActionResult ThongTinCaNhan(FormCollection f)
        {
            TaiKhoan TK = Session["TaiKhoan"] as TaiKhoan;
            if (TK != null)
            {
                NguoiDung ND = db.NguoiDungs.Include("TaiKhoans").FirstOrDefault(t => t.ID == TK.ID);
                bool check = true;
                if (f["Email"] == "" || f["Email"] == null)
                {
                    ViewData["Email"] = "Email không được để trống";
                    check = false;
                }
                else
                {
                    if (!HoTro.IsEmail(f["Email"]))
                    {
                        ViewData["Email"] = "Email không đúng định dạng, hãy nhập lại";
                        check = false;
                    }
                    else
                        ND.Email = f["Email"];
                }
                ND.AnhDaiDien = f["AnhMoi"];
                if (string.IsNullOrEmpty(f["HoVaTen"]) || f["HoVaTen"] == null)
                {
                    ViewData["HoVaTen"] = "Họ tên của bạn không được để trống";
                    check = false;
                }
                else
                    ND.TenNguoiDung = f["HoVaTen"];
                ND.NickName = f["NickName"];
                if (f["NgaySinh"] != null & f["NgaySinh"] != "")
                {

                    DateTime a = DateTime.Parse(f["NgaySinh"]);
                    if (a >= DateTime.Now)
                    {
                        ViewData["NgaySinh"] = "Ngày sinh phải nhỏ hơn ngày hiện tại";
                        check = false;
                    }
                    else
                        ND.NgaySinh = a;
                }
                if (f["GioiTinh"] != null)
                {
                    ND.GioiTinh = f["GioiTinh"];
                }
                if (f["SoDienThoai"] == null || string.IsNullOrEmpty(f["SoDienThoai"]))
                {
                    ViewData["SoDienThoai"] = "Số điện thoại không được để trống";
                    check = false;
                }
                else
                {
                    if (f["SoDienThoai"].Length < 10)
                    {
                        ViewData["SoDienThoai"] = "Số điện thoại phải có 10 ký tự";
                        check = false;
                    }
                    else
                        ND.SoDienThoai = f["SoDienThoai"];
                }
                if (check)
                {
                    db.NguoiDungs.Attach(ND);
                    db.Entry(ND).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.thanhcong = 1;
                }

                return View(ND);
            } 
            else
                return RedirectToAction("TrangChu", "Home");
        }
    }

}