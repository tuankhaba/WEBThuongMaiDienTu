using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Controllers
{
    public class CuaHangController : Controller
    {
        ThuongMaiDienTuEntities db = new ThuongMaiDienTuEntities();
        // GET: CuaHang
        public ActionResult TrangCuaHang(string MaCH)
        {
            Session["lsp"] = db.LoaiSanPhams.ToList();
            CuaHang ch = db.CuaHangs.Single(c => c.MaCuaHang.Equals(MaCH));
            ch.LuotTruyCap++;
            db.SaveChanges();
            return View(ch);
        }
        public ActionResult TrangCuaHang_TrangChu()
        {
            Session["lsp"] = db.LoaiSanPhams.ToList();
            return View();
        }
        public ActionResult TrangCuaHang_TatCaSanPham()
        {
            return View();
        }
        public ActionResult TrangCuaHang_ThongTinShop()
        {
            return View();
        }
    }
}