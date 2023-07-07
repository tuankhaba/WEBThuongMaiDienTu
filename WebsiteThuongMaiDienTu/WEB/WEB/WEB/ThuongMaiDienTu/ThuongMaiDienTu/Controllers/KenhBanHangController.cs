using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Controllers
{
    public class KenhBanHangController : Controller
    {
        ThuongMaiDienTuEntities db = new ThuongMaiDienTuEntities();
        // GET: KenhBanHang
        public ActionResult KenhBanHang()
        {
            //Session["TaiKhoan"] = db.TaiKhoans.Single(t => t.ID.Equals("ND001"));
            if (Session["TaiKhoan"]==null)
                return RedirectToAction("DangNhap", "Home");
            else
            {
                TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;
                if(db.CuaHangs.Count(c=>c.MaCuaHang.Equals(tk.ID))==0)
                {
                    return RedirectToAction("DangKyCuaHang", "KenhBanHang");
                }
                CuaHang Ch = db.CuaHangs.Single(ch => ch.MaCuaHang.Equals(tk.ID));
                Session["KenhBanHang"] = Ch;
                return RedirectToAction("DoanhThu", "KenhBanHang");
            }
        }
        public ActionResult DangXuatCuaHang()
        {
            Session.Remove("KenhBanHang");
            return RedirectToAction("TrangChu", "Home");
        }
        public ActionResult DangKyCuaHang()
        {
            //Session["TaiKhoan"] = db.TaiKhoans.Single(t => t.ID.Equals("ND003"));
            TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;
            ViewBag.Tinh = db.Tinh_ThanhPho.ToList();
            ViewBag.Quan = db.Quan_Huyen.ToList();
            ViewBag.Xa = db.Xa_Phuong.ToList();
            NguoiDung nd = db.NguoiDungs.Include("ThongTinLienHes").Single(t => t.ID == tk.ID);
            return View(nd);
        }
        public ActionResult XoaSanPham(string Masp)
        {
            int a = db.XoaSanPham(Masp);
            return RedirectToAction("TatCaSanPham");
        }
        public ActionResult AnSanPham(string Masp)
        {
            SanPham sp = db.SanPhams.Single(s => s.MaSP.Equals(Masp));
            sp.TrangThai = false;
            db.SanPhams.Attach(sp);
            db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("TatCaSanPham");
        }
        public ActionResult HienSanPham(string Masp)
        {
            SanPham sp = db.SanPhams.Single(s => s.MaSP.Equals(Masp));
            sp.TrangThai = true;
            db.SanPhams.Attach(sp);
            db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("TatCaSanPham");
        }
        public ActionResult KhuyenMai()
        {
            //Session["TaiKhoan"] = db.TaiKhoans.Single(t => t.ID.Equals("ND001"));


            TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;
            List<KhuyenMai> Sales = db.KhuyenMais.Where(k => k.MaCuaHang.Equals(tk.ID)).ToList();            
            return View(Sales);
        }
        public ActionResult TaoKhuyenMai()
        {            
            return View();
        }
        public ActionResult QuanLyDonHang()
        {
            TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;
            if (tk != null)
            {                
                List<DonHang> DHs = db.DonHangs.Where(d => d.MaCuaHang.Equals(tk.ID)).ToList();
                return View(DHs);
            }
            return RedirectToAction("TrangChu", "Home");
        }
        [HttpPost]
        public ActionResult QuanLyDonHang(string txt_search)
        {
            TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;
            if (tk != null)
            {
                ViewBag.txt = txt_search;
                List<DonHang> DHs = db.DonHangs.Where(d => d.MaCuaHang.Equals(tk.ID) && d.MaDonHang.Contains(txt_search)).ToList();
                return View(DHs);
            }
            return RedirectToAction("TrangChu", "Home");
        }
        [HttpGet]
        public ActionResult XacNhanDonHang(string maDH, string url)
        {
            TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;
            if (tk != null)
            {
                DonHang DH = db.DonHangs.Single(d => d.MaDonHang.Equals(maDH));
                DH.TrangThai = "Đang xử lý";
                db.Entry(DH).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Redirect(url);
            }
            return RedirectToAction("TrangChu", "Home");
        }
        public ActionResult HoanTatXuLyDonHang(string maDH, string url)
        {
            TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;
            if (tk != null)
            {
                DonHang DH = db.DonHangs.Single(d => d.MaDonHang.Equals(maDH));
                DH.TrangThai = "Đang giao";
                db.Entry(DH).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Redirect(url);
            }
            return RedirectToAction("TrangChu", "Home");
        }
        public ActionResult XacNhanHangLoat(string url)
        {
            TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;
            if (tk != null)
            {
                List<DonHang>DHs = db.DonHangs.Where(d => d.MaCuaHang.Equals(tk.ID)&&d.TrangThai.Equals("Chờ xác nhận")).ToList();
                foreach(var item in DHs)
                {
                    item.TrangThai = "Đang xử lý";
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }    
                db.SaveChanges();
                return Redirect(url);
            }
            return RedirectToAction("TrangChu", "Home");
        }
        
        public ActionResult VanChuyenHoanTat(string maDH, string url)
        {
            TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;
            if (tk != null)
            {
                DonHang DH = db.DonHangs.Single(d => d.MaDonHang.Equals(maDH));
                DH.TrangThai = "Đã giao";
                db.Entry(DH).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Redirect(url);
            }
            return RedirectToAction("TrangChu", "Home");
        }
        public ActionResult TuChoiDonHang(string maDH, string url)
        {
            TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;
            if (tk != null)
            {
                DonHang DH = db.DonHangs.Single(d => d.MaDonHang.Equals(maDH));
                DH.TrangThai = "Bị từ chối";
                db.Entry(DH).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Redirect(url);
            }
            return RedirectToAction("TrangChu", "Home");
        }

        //public ActionResult ShowChiTietDonHang()
        //{
        //    IEnumerable<ChiTietDonHang> Dh = null;
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("https://localhost:44304/api/");
        //        var responseTask = client.GetAsync("DonHangApi/");
        //        responseTask.Wait();
        //        var result = responseTask.Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            var readSP = result.Content.ReadAsAsync<IList<ChiTietDonHang>>();
        //            readSP.Wait();
        //            Dh = readSP.Result;
        //        }
        //        else
        //        {
        //            Dh = Enumerable.Empty<ChiTietDonHang>();
        //            ModelState.AddModelError(string.Empty, "Erorr occured !!!");
        //        }
        //        return View(Dh);
        //    }
        //}

        public ActionResult ShowChiTietDonHang(string maDH)
        {
            DonHang Dh = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44304/api/");
                var responseTask = client.GetAsync("DonHangApi/ShowChiTietDonHang?maDH="+maDH);
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
        public ActionResult DoanhThu()
        {
            TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;
            if (tk != null)
            {
                CuaHang ch = db.CuaHangs.Single(c => c.MaCuaHang.Equals(tk.ID));
                CuaHangApiController ChApi = new CuaHangApiController();
                ViewBag.Ch = ch;
                ViewBag.DL = ChApi.GetDSThongKeDoanhThu(ch, null, ch.NgayDangKy, DateTime.Now);
                try
                {
                    ViewBag.TongDT = db.DonHangs.Where(d=>d.MaCuaHang.Equals(ch.MaCuaHang)).Sum(d => d.ThanhTien);
                    ViewBag.tongCH = ViewBag.TongDT;
                    ViewBag.tongALLSP = ViewBag.TongDT;
                    ViewBag.tongSP = ViewBag.TongDT;
                }
                catch
                {
                    ViewBag.TongDT = 0;
                    ViewBag.TongCH = 0;
                    ViewBag.tongALLSP = 0;
                    ViewBag.tongSP = 0;
                }
                return View();
            }
            return RedirectToAction("TrangChu", "Home");
            
        }
        [HttpPost]
        public ActionResult DoanhThu(FormCollection f)
        {
            TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;
            if (tk != null)
            {
                CuaHang ch = db.CuaHangs.Single(c => c.MaCuaHang.Equals(tk.ID));
                CuaHangApiController ChApi = new CuaHangApiController();
                SanPham a = null;
                DateTime batdau = ch.NgayDangKy;
                DateTime ketthuc = DateTime.Now;
                try
                {
                    if (f["NgayBatDau"] != null && f["NgayBatDau"] != "")
                        batdau = DateTime.Parse(f["NgayBatDau"]);
                    if (f["NgayKetThuc"] != null && f["NgayKetThuc"] != "")
                        ketthuc = DateTime.Parse(f["NgayKetThuc"]);
                }
                catch { }

                    string ma = f["MaSP"];
                    if (!ma.Equals("0"))
                        a = ch.SanPhams.Single(s=>s.MaSP.Equals(f["MaSP"]));
                DataThongKe dt = ChApi.GetDSThongKeDoanhThu(ch, a, batdau, ketthuc);
                ViewBag.DL = dt;             
                ViewBag.Ch = ch;
                ViewBag.tongCH = dt.Datas.Sum(t => t.CuaHang);
                try
                {
                    ViewBag.TongDT = db.DonHangs.Where(d => d.MaCuaHang.Equals(ch.MaCuaHang)).Sum(d => d.ThanhTien);                  
                    ViewBag.tongALLSP = ViewBag.TongDT;
                }catch
                {
                    ViewBag.TongDT = 0;
                    ViewBag.tongALLSP = 0;
                }
                if (a != null)
                    ViewBag.tongSP = dt.Datas.Sum(t => t.SanPham);
                return View();
            }
            return RedirectToAction("TrangChu", "Home");
        }
        public ActionResult NganHang()
        {
            
            return View();
        }
        public ActionResult ThanhToan()
        {
            
            return View();
        }
        public ActionResult PhanTichDuLieu()
        {
           
            return View();
        }
        public ActionResult KhachHangDaMua()
        {
            
            return View();
        }
        public ActionResult TroLyChat()
        {
            
            return View();
        }
        public ActionResult HoiDap()
        {
            
            return View();
        }
        public ActionResult HoSo()
        {
            TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;
            if (tk != null)
            {
                return View(db.CuaHangs.Single(c => c.MaCuaHang.Equals(tk.ID)));
            }
            else
            {
                return RedirectToAction("TrangChu", "Home");
            }    
        }
        [HttpPost]
        public ActionResult HoSo(FormCollection f)
        {

            TaiKhoan TK = Session["TaiKhoan"] as TaiKhoan;
            if (TK != null)
            {
                CuaHang CH = db.CuaHangs.Single(c => c.MaCuaHang.Equals(TK.ID));
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
                        CH.NguoiDung.Email = f["Email"];
                }
                CH.AnhDaiDien = f["AnhMoi"];
                if (string.IsNullOrEmpty(f["TenCuaHang"]) || f["TenCuaHang"] == null)
                {
                    ViewData["TenCuaHang"] = "Tên cửa hàng không được để trống";
                    check = false;
                }
                else
                    CH.TenCuaHang = f["TenCuaHang"];
                CH.ChuKy = f["Chuky"];                
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
                        CH.SoDienThoai = f["SoDienThoai"];
                }
                if (check)
                {
                    db.CuaHangs.Attach(CH);
                    db.Entry(CH).State = System.Data.Entity.EntityState.Modified;
                    db.NguoiDungs.Attach(CH.NguoiDung);
                    db.Entry(CH.NguoiDung).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.thanhcong = 1;
                }         
                return View(CH);
            }
            else
            {
                return RedirectToAction("TrangChu", "Home");
            }
        }
        public ActionResult DanhGia()
        {
            return View();
        }
        public ActionResult ThietLap()
        {          
            return View();
        }
        [HttpPost]
        public ActionResult XoaKhuyenMai(string MaKm)
        {
            KhuyenMai km = db.KhuyenMais.Single(k => k.MaGiamGia.Equals(MaKm));
            db.Entry(km).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("KhuyenMai");
        }
        [HttpPost]
        public ActionResult DangKyCuaHang(FormCollection f)
        {
            //Session["TaiKhoan"] = db.TaiKhoans.Single(t => t.ID.Equals("ND003"));
            TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;
            CuaHang ch = new CuaHang();
            ch.MaCuaHang = tk.ID;
            ch.SoDienThoai = tk.NguoiDung.SoDienThoai;
            ch.TenCuaHang = HoTro.ToFirstUpper(f["TenCuaHang"]);
            ch.DiaChiLayHang = f["XaPhuong"];
            ch.NguoiDung = db.NguoiDungs.Single(t => t.ID == tk.ID);
            ch.NgayDangKy = DateTime.Now;
            db.CuaHangs.Add(ch);
            db.SaveChanges();
            return RedirectToAction("KenhBanHang", "KenhBanHang");
        }
        [HttpGet]
        public ActionResult TrangChu()
        {
            
            return View();
        }
        [HttpGet]
        public ActionResult ThemSanPham()
        {
            //Session["TaiKhoan"] = db.TaiKhoans.Single(t => t.ID.Equals("ND001"));

            TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;            
            ViewBag.LoaiSP = db.LoaiSanPhams.ToList();
            ViewBag.CTLoaiSP = db.ChiTietLoaiSanPhams.ToList();
            return View(); 
        }
        [HttpPost]
        public ActionResult ThemSanPham(FormCollection f)
        {
            //Session["TaiKhoan"] = db.TaiKhoans.Single(t => t.ID.Equals("ND001"));



            TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;
            SanPham sp = new SanPham();
            CuaHang ch = db.CuaHangs.Single(c => c.MaCuaHang == tk.ID);
            int sosp = ch.SanPhams.Count();
            do
            {
                sp.MaSP = tk.ID.Trim() + "SP0" + (sosp++).ToString();
            } while (ch.SanPhams.Count(s => s.MaSP.Equals(sp.MaSP))!=0);
            sp.MaCuaHang = tk.ID;
            sp.TenSP = f["TenSP"].Trim();
            sp.MaChiTietLoai = f["ChiTietNganhHang"];
            if (f["GiamGia"] == null || f["GiamGia"] == "")
                sp.GiamGia = 0;
            else
                sp.GiamGia = int.Parse(f["GiamGia"]);
            if (sp.GiamGia > 0)
                sp.ThoiHan = DateTime.Parse(f["ThoiHan"].ToString());
            sp.Xoa = false;
            sp.TrangThai = true;
            sp.AnhBia = f["Anh1"];
            sp.SoLuongDanhGia = 0;
            sp.DanhGia = 0;
            sp.MoTaSP = f["MoTa"];
            int slAnh = int.Parse(f["SoLuongAnh"]);
            db.SanPhams.Add(sp);
            int sl = db.AnhSanPhams.Count(ah => ah.MaSP.Equals(sp.MaSP)) + 1;
            for (int i=1;i<slAnh;i++)
            {
                AnhSanPham a = new AnhSanPham();
                a.MaSP = sp.MaSP;
                a.SanPham = sp;
                a.MaAnh = sp.MaSP + "ANH0"+ (sl++).ToString();
                a.DuongDanAnh = f["Anh" + (i + 1).ToString()];
                db.AnhSanPhams.Add(a);
            }
            int slLoai = int.Parse(f["SoLuongLoai"]);
            for(int i=1; i<=slLoai;i++)
            {
                ChiTietPhanLoai ctpl = new ChiTietPhanLoai();
                ctpl.MaCTPhanLoai = sp.MaSP + "CTPL" + (i).ToString();
                ctpl.MaSP = sp.MaSP;
                ctpl.SanPham = sp;
                ctpl.TenPhanLoai = f["TenLoai" + i.ToString().Trim()].Trim();
                ctpl.Gia = int.Parse(f["GiaLoai" + i.ToString()]);
                int slKCLoai = int.Parse(f["SoLuongKichCoLoai"+i.ToString()]);
                db.ChiTietPhanLoais.Add(ctpl);
                for(int j=1;j<=slKCLoai;j++)
                {
                    ChiTietKichCo ct = new ChiTietKichCo();
                    ct.MaCTKichCo = ctpl.MaCTPhanLoai + "CTKC" + j.ToString();
                    ct.ChiTietPhanLoai = ctpl;
                    ct.KichCo = f["Loai" + i.ToString() + "TenKichCo" + j.ToString()].Trim();
                    ct.SoLuong = int.Parse(f["Loai" + i.ToString() + "SLKichCo" + j.ToString()]);
                    db.ChiTietKichCoes.Add(ct);
                }
            }
            int sl_thuoctinh = int.Parse(f["SlThuocTinh"]);
            for(int i=1;i<=sl_thuoctinh;i++)
            {
                ChiTietThongTinSP ctsp = new ChiTietThongTinSP();
                ctsp.SanPham = sp;
                ctsp.MaSP = sp.MaSP;
                ctsp.ThuocTinh = f["ThuocTinh" + i.ToString()].Trim();
                ctsp.NoiDung = f["NoiDung" + i.ToString()].Trim();
                db.ChiTietThongTinSPs.Add(ctsp);
            }
            db.SaveChanges();
            ViewBag.ThanhCong = 1;
           
            ViewBag.LoaiSP = db.LoaiSanPhams.ToList();
            ViewBag.CTLoaiSP = db.ChiTietLoaiSanPhams.ToList();
            return View();
        }
        public ActionResult TatCaSanPham(int? page, int? pageSize)
        {
            TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;     
                if (page == null)
                {
                    page = 1;
                }
                if (pageSize == null)
                {
                    pageSize = 2;
                }
                List<SanPham> List_sp = db.SanPhams.Where(sp => sp.MaCuaHang.Equals(tk.ID)).Where(sp => sp.Xoa == false).ToList();
            return View(List_sp.ToPagedList((int)page, (int)pageSize));
        }
        [HttpPost]
        public ActionResult TatCaSanPham(string txt_search)
        {
            //Session["TaiKhoan"] = db.TaiKhoans.Single(t => t.ID.Equals("ND001"));


            TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;
            return View(db.SanPhams.Where(sp => sp.MaCuaHang.Equals(tk.ID)).Where(s=>s.TenSP.Contains(txt_search)).Where(sp => sp.Xoa == false));
        }
        public ActionResult SuaSanPham(string Masp)
        {
            ViewBag.LoaiSP = db.LoaiSanPhams.ToList();
            ViewBag.CTLoaiSP = db.ChiTietLoaiSanPhams.ToList();
            return View(db.SanPhams.Single(sp => sp.MaSP.Equals(Masp)));
        }
        [HttpPost]
        public ActionResult SuaSanPham(string Masp, FormCollection f)
        {
            SanPham sp = db.SanPhams.Single(s => s.MaSP.Equals(Masp));
            sp.TenSP = f["TenSP"].Trim();
            sp.MaChiTietLoai = f["ChiTietNganhHang"];
            if (f["GiamGia"] == null || f["GiamGia"] == "")
                sp.GiamGia = 0;
            else
                sp.GiamGia = int.Parse(f["GiamGia"]);
            if (sp.GiamGia > 0)
                sp.ThoiHan = DateTime.Parse(f["ThoiHan"].ToString());
            sp.AnhBia = f["Anh1"];
            sp.MoTaSP = f["MoTa"];
            db.SanPhams.Attach(sp);
            db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
            List<AnhSanPham> asp = sp.AnhSanPhams.ToList();
            foreach (AnhSanPham item in asp)
                db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
            List<ChiTietPhanLoai> ctpls = db.ChiTietPhanLoais.Where(t => t.MaSP.Equals(Masp)).ToList();
            foreach (ChiTietPhanLoai item in ctpls)
                db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
            List<ChiTietKichCo> kcs = db.ChiTietKichCoes.Where(c => c.ChiTietPhanLoai.MaSP.Equals(Masp)).ToList();
            foreach (ChiTietKichCo item in kcs)
                db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
            List<ChiTietThongTinSP> ctsps = db.ChiTietThongTinSPs.Where(c => c.MaSP.Equals(Masp)).ToList();
            foreach (ChiTietThongTinSP item in ctsps)
                db.Entry(item).State = System.Data.Entity.EntityState.Deleted;

            int slAnh = int.Parse(f["SoLuongAnh"]);
            int sl = db.AnhSanPhams.Count(ah => ah.MaSP.Equals(sp.MaSP)) + 1;
            for (int i = 1; i < slAnh; i++)
            {
                AnhSanPham a = new AnhSanPham();
                a.MaSP = sp.MaSP;
                a.SanPham = sp;
                a.MaAnh = sp.MaSP + "ANH0" + (sl++).ToString();
                a.DuongDanAnh = f["Anh" + (i + 1).ToString()];
                db.AnhSanPhams.Add(a);
            }
            int slLoai = int.Parse(f["SoLuongLoai"]);
            int id = 1;
            for (int i = 1; i <= slLoai; i++)
            {
                ChiTietPhanLoai ctpl = new ChiTietPhanLoai();                
                do
                {
                    ctpl.MaCTPhanLoai = sp.MaSP + "CTPL" + (id++).ToString();
                } while (db.ChiTietPhanLoais.Count(p=>p.MaCTPhanLoai.Equals(ctpl.MaCTPhanLoai)) != 0);
                ctpl.MaSP = sp.MaSP;
                ctpl.SanPham = sp;
                ctpl.TenPhanLoai = f["TenLoai" + i.ToString().Trim()];
                ctpl.Gia = int.Parse(f["GiaLoai" + i.ToString()]);
                int slKCLoai = int.Parse(f["SoLuongKichCoLoai" + i.ToString()]);
                db.ChiTietPhanLoais.Add(ctpl);
                for (int j = 1; j <= slKCLoai; j++)
                {
                    ChiTietKichCo ct = new ChiTietKichCo();
                    ct.MaCTKichCo = ctpl.MaCTPhanLoai + "CTKC" + j.ToString();
                    ct.ChiTietPhanLoai = ctpl;
                    ct.KichCo = f["Loai" + i.ToString() + "TenKichCo" + j.ToString()].Trim();
                    ct.SoLuong = int.Parse(f["Loai" + i.ToString() + "SLKichCo" + j.ToString()]);
                    db.ChiTietKichCoes.Add(ct);
                }
            }
            int sl_thuoctinh = int.Parse(f["SlThuocTinh"]);
            for (int i = 1; i <= sl_thuoctinh; i++)
            {
                ChiTietThongTinSP ctsp = new ChiTietThongTinSP();
                ctsp.SanPham = sp;
                ctsp.MaSP = sp.MaSP;
                ctsp.ThuocTinh = f["ThuocTinh" + i.ToString()].Trim();
                ctsp.NoiDung = f["NoiDung" + i.ToString()].Trim();
                db.ChiTietThongTinSPs.Add(ctsp);
            }
            db.SaveChanges();
            Session["check"] = true;
            return RedirectToAction("TatCaSanPham");
        }
        [HttpPost]
        public ActionResult TaoKhuyenMai(FormCollection f)
        {
            TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;
            if (tk != null)
            {
                KhuyenMai moi = new KhuyenMai()
                {
                    MaGiamGia = "MaKM" + (db.KhuyenMais.Count() + 1).ToString(),
                    MaCuaHang = tk.ID,
                    MaVoucher = f["MaKM"],
                    TenKhuyenMai = f["TenCT"],
                    TyLeGiamGia = double.Parse(f["TyLeGiamGia"]) / 100,
                    DonHangToiThieu = int.Parse(f["ToiThieu"]),
                    LuotSuDung = int.Parse(f["LuotSuDung"]),
                    MucGiamToiDa = int.Parse(f["MuaGiamToiDa"]),
                    NgayKetThuc = DateTime.Parse(f["NgayKetThuc"])
                };
                if (f["NgayBatDau"] == null || f["NgayBatDau"] == "")
                    moi.NgayBatDau = DateTime.Now;
                else
                    moi.NgayBatDau = DateTime.Parse(f["NgayBatDau"]);
                try
                {
                    db.KhuyenMais.Add(moi);
                    db.SaveChanges();
                }
                catch
                {
                    ViewBag.kq = false;
                }
                return View();
            }
            else
                return RedirectToAction("TrangChu", "Home");
        }
        public ActionResult SuaKhuyenMai(string Ma)
        {
            KhuyenMai km = db.KhuyenMais.Single(k => k.MaGiamGia.Equals(Ma));
            return View(km);
        }
        [HttpPost]
        public ActionResult SuaKhuyenMai( string Ma, FormCollection f)
        {
            KhuyenMai km = db.KhuyenMais.Single(k => k.MaGiamGia.Equals(Ma));
            if (ModelState.IsValid)
            {
                km.MaVoucher = f["MaKM"];
                km.TenKhuyenMai = f["TenCT"];
                km.TyLeGiamGia = double.Parse(f["TyLeGiamGia"]) / 100;
                km.DonHangToiThieu = int.Parse(f["ToiThieu"]);
                km.LuotSuDung = int.Parse(f["LuotSuDung"]);
                km.MucGiamToiDa = int.Parse(f["MuaGiamToiDa"]);
                km.NgayKetThuc = DateTime.Parse(f["NgayKetThuc"]);
                if (f["NgayBatDau"] == null || f["NgayBatDau"] == "")
                    km.NgayBatDau = DateTime.Now;
                else
                    km.NgayBatDau = DateTime.Parse(f["NgayBatDau"]);
            }
            else
                ViewBag.thaotac = false;
            try
            {
                db.KhuyenMais.Attach(km);
                db.Entry(km).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                ViewBag.kq = false;
            }
            return RedirectToAction("KhuyenMai", "KenhbanHang");
        }
    }
}