using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Controllers
{
    public class GioHangController : Controller
    {
        ThuongMaiDienTuEntities db = new ThuongMaiDienTuEntities();
        // GET: GioHang
        public ActionResult GioHang()
        {
            TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;
            //tk = db.TaiKhoans.Single(t => t.ID.Equals("ND002"));
            //Session["TaiKhoan"] = tk;
            if (tk == null)
            {
                return View(HoTro.LayDSGioHangChiaTheoCuaHang(LayGioHang()));
            }
            List<ChiTietGioHang> GHs = db.ChiTietGioHangs.Include("ChiTietKichCo").Where(nd => nd.ID == tk.ID).ToList();            
            return View(HoTro.LayDSGioHangChiaTheoCuaHang(GHs));
        }
        public List<ChiTietGioHang> LayGioHang()
        {
            List<ChiTietGioHang> lgh = Session["giohang"] as List<ChiTietGioHang>;
            if (lgh == null)
            {
                lgh = new List<ChiTietGioHang>();
                Session["giohang"] = lgh;
            }            
            return lgh;
        }

        public ActionResult ThemSanPhamVaoGioHang(string MaKichCo, int Sl, string url, int YeuCau)
        {
            TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;
            if (tk == null)
            {
                try
                {
                    List<ChiTietGioHang> GHs = LayGioHang();
                    if (GHs.Count(sp => sp.MaCTKichCo.Equals(MaKichCo)) == 0)
                    {
                        GHs.Add(new ChiTietGioHang()
                        {
                            MaCTKichCo = MaKichCo,
                            SoLuong = Sl,
                            ChiTietKichCo = db.ChiTietKichCoes.Single(kc => kc.MaCTKichCo.Equals(MaKichCo)),                            
                        });
                        Session["slSp_ghang"] = GHs.Sum(s => s.SoLuong);
                    }
                    else
                    {
                        ChiTietGioHang Chkgh = GHs.Single(sp => sp.MaCTKichCo.Equals(MaKichCo));
                        int sl = db.ChiTietKichCoes.Single(sp => sp.MaCTKichCo.Equals(MaKichCo)).SoLuong;
                        if (Chkgh.SoLuong + Sl > sl)
                            Session["LoiThemGH"] = true;
                        else
                        {
                            Chkgh.SoLuong += Sl;
                            Session["slSp_ghang"] = GHs.Sum(s => s.SoLuong);
                        }
                    }
                }
                catch
                {
                    Session["LoiThemGH"] = true;
                }
            }
            else
            {
                try
                {
                    db.ThemSPVaoGioHang(tk.ID, MaKichCo, Sl);
                    db.SaveChanges();
                }
                catch
                {
                    Session["LoiThemGH"] = true;
                }
                Session["slSp_ghang"] = db.ChiTietGioHangs.Where(g => g.ID.Equals(tk.ID)).Sum(s => s.SoLuong);
            }
            if (YeuCau == 1)
                return Redirect(url);
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaKhoiGioHang(string mkc, string url)
        {
            TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;
            if (tk != null)
            {
                ChiTietGioHang gh = db.ChiTietGioHangs.Single(g => g.MaCTKichCo.Equals(mkc) && g.ID.Equals(tk.ID));                
                db.Entry(gh).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                try
                {
                    Session["slSp_ghang"] = db.ChiTietGioHangs.Where(g => g.ID.Equals(tk.ID)).Sum(g => g.SoLuong);
                }
                catch
                {
                    Session["slSp_ghang"] = 0;
                }
            }
            else
            {
                List<ChiTietGioHang> GHs = LayGioHang();
                GHs.Remove(GHs.Single(g => g.MaCTKichCo.Equals(mkc)));
                Session["slSp_ghang"] = GHs.Sum(s => s.SoLuong);
            }
            return Redirect(url);
        }
        public List<ChiTietDonHangTam> LayChiTietDonHangTam()
        {
            List<ChiTietDonHangTam> CTDH = Session["CTDH"] as List<ChiTietDonHangTam>;
            if (CTDH == null)
            {
                CTDH = new List<ChiTietDonHangTam>();
                Session["CTDH"] = CTDH;
            }
            return CTDH;
        }
        public ActionResult TaoDonHang()
        {
            TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;
            if (tk != null)
            {
                List<ChiTietDonHangTam> CTDHs = LayChiTietDonHangTam();
                string masp = CTDHs[0].MaSP;
                string MaCH = db.SanPhams.Single(s => s.MaSP.Equals(masp)).CuaHang.MaCuaHang;
                ViewBag.TTLH = db.ThongTinLienHes.Where(l => l.ID.Equals(tk.ID) && !l.An.Value).ToList();
                ViewBag.CuaHang = db.CuaHangs.Single(c => c.MaCuaHang.Equals(MaCH));
                ViewBag.TongTien = HoTro.TinhTongTien(CTDHs);
                return View(CTDHs);
            }
            else
                return RedirectToAction("TrangChu", "Home");
        }
        [HttpPost]
        public ActionResult TaoDonHang(FormCollection f, string lurl)
        {
            TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;
            if(tk!=null)
            {                
                List<ChiTietGioHang> GHs = db.ChiTietGioHangs.Where(g => g.ID.Equals(tk.ID)).ToList();
                GHs.ForEach(item => item.ChiTietKichCo = db.ChiTietKichCoes.Single(ct => ct.MaCTKichCo.Equals(item.MaCTKichCo)));
                long tong = 0;
                string MaCH ="";
                List<ChiTietDonHangTam> CTDHs = LayChiTietDonHangTam();                
                    CTDHs.Clear();
                DateTime hientai = DateTime.Now;
                for (int i = 0; i < GHs.Count(); i++)
                {
                    string MaKc = f["MaKC" + i.ToString()];
                    if (MaKc != null)
                    {
                        ChiTietGioHang gh = GHs.Single(s => s.MaCTKichCo.Trim().Equals(MaKc.Trim()));
                        int gia = gh.ChiTietKichCo.ChiTietPhanLoai.Gia;
                        if (gh.ChiTietKichCo.ChiTietPhanLoai.SanPham.ThoiHan >= hientai)
                            gia = gia - Convert.ToInt32(gia * gh.ChiTietKichCo.ChiTietPhanLoai.SanPham.GiamGia / 100);
                        CTDHs.Add(new ChiTietDonHangTam(gh.ChiTietKichCo.ChiTietPhanLoai.MaSP, gh.ChiTietKichCo.ChiTietPhanLoai.SanPham.TenSP, MaKc, gh.ChiTietKichCo.KichCo, gh.ChiTietKichCo.ChiTietPhanLoai.TenPhanLoai, gh.ChiTietKichCo.ChiTietPhanLoai.Gia, int.Parse(f["Sl" + i.ToString()])));
                        tong += gia * int.Parse(f["Sl" + i.ToString()]);
                        MaCH = gh.ChiTietKichCo.ChiTietPhanLoai.SanPham.MaCuaHang;
                    }
                }
                ViewBag.TTLH = db.ThongTinLienHes.Where(l => l.ID.Equals(tk.ID) && !l.An.Value).ToList();
                ViewBag.CuaHang = db.CuaHangs.Single(c=>c.MaCuaHang.Equals(MaCH));
                ViewBag.TongTien = tong;
                return View(CTDHs);
            }
            Session["url"] = lurl;
            return RedirectToAction("DangNhap", "Home", new { url = lurl });
        }        
        [HttpPost]
        public ActionResult TaoThanhCongDonHang(FormCollection f)
        {
            TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;
            DonHang DHmoi = new DonHang();
            List<ChiTietDonHangTam> CTDHs = LayChiTietDonHangTam();                       
            int j = 1;
            do
            {
                DHmoi.MaDonHang = tk.ID.Trim() + "DH0" + (db.DonHangs.Count(t => t.ThongTinLienHe.ID.Equals(tk.ID)) + j++).ToString();
            } while (db.DonHangs.Count(t => t.MaDonHang.Equals(DHmoi.MaDonHang)) > 0);
            DHmoi.MaCuaHang = f["MaCH"];
            DHmoi.NgayMua = DateTime.Now;
            DHmoi.TrangThai = "Chờ xác nhận";
            DHmoi.ThanhTien = int.Parse(f["ThanhTien"]);
            if (f["MaVoucher"] != null && f["MaVoucher"].Trim()!="")
                DHmoi.MaVoucher = f["MaVoucher"];
            DHmoi.PhuongThucGiao = f["GiaoHang"];
            DHmoi.PhuongThucThanhToan = f["ThanhToan"];
            DHmoi.MaLH = f["MaLH"];
            if (f["LoiNhan"] != null)
                DHmoi.LoiNhan = f["LoiNhan"];
            db.DonHangs.Add(DHmoi);                                                     
            foreach(var item in CTDHs)
            {
                db.ChiTietDonHangs.Add(new ChiTietDonHang()
                {
                    MaCTKichCo = item.MaCTKichCo,
                    MaDonHang = DHmoi.MaDonHang,
                    MaSP = item.MaSP,
                    SoLuong = item.SoLuong
                });
                db.XoaChiTietGioHangDuaVaoCTDonHang(tk.ID, item.MaCTKichCo, item.SoLuong);
            }    
            db.SaveChanges();
            try
            {
                Session["slSp_ghang"] = db.ChiTietGioHangs.Where(g => g.ID.Equals(tk.ID)).Sum(s => s.SoLuong);
            }
            catch
            {
                Session["slSp_ghang"] = 0;
            }
            Session.Remove("CTDH");
            ViewBag.maDH = DHmoi.MaDonHang;
            return View();
        }
    }
}