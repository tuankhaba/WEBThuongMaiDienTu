using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Controllers
{
    public class SanPhamController : Controller
    {
        ThuongMaiDienTuEntities db = new ThuongMaiDienTuEntities();
        // GET: SanPham
        public ActionResult ThongTinSanPham(string MaSP)
        {
            Session["lsp"] = db.LoaiSanPhams.ToList();
            ViewBag.SanPhams = db.SanPhams.Where(s=>s.Xoa==false).ToList();
            SanPham sp = db.SanPhams.Single(s => s.MaSP.Equals(MaSP));
            return View(sp);
        }
        [HttpPost]
        public ActionResult DanhGiaSanPham(string MaSP, FormCollection f)
        {
            TaiKhoan TK = Session["TaiKhoan"] as TaiKhoan;
            if (TK != null)
            {
                SanPham sp = db.SanPhams.Single(s => s.MaSP.Equals(MaSP));
                int muc = int.Parse(f["MucDG"]);
                if (muc == 0)
                {
                    ViewBag.LoiSao = "Vui lòng đánh giá mức sao";
                    return View(sp);
                }                
                bool an = false;
                if (f["AnDanh"] != null)
                    an = true;                                
                string nd = "";
                for (int i =0;i< 3;i++)                
                    if (f["mau" + i.ToString()] != null)
                        nd += f["mau" + i.ToString()]+", ";
                nd += f["NoiDung"] == null? "": f["NoiDung"];
                db.ThemDanhGia(TK.ID, muc, nd, MaSP, an);
                db.SaveChanges();
                return RedirectToAction("QuanLyDonHang", "TaiKhoanCaNhan");
            }
            else
                return RedirectToAction("TrangChu", "Home");

            
        }        
        public ActionResult DanhGiaSanPham(string MaSP)
        {
            TaiKhoan TK = Session["TaiKhoan"] as TaiKhoan;
            if (TK != null)
            {
                return View(db.SanPhams.Single(sp => sp.MaSP.Equals(MaSP)));
            }
            else
                return RedirectToAction("TrangChu", "Home");
        }
    }
}