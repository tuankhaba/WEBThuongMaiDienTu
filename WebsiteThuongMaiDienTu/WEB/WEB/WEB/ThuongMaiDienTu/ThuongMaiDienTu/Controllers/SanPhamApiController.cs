using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Controllers
{
    public class SanPhamApiController : ApiController
    {
        ThuongMaiDienTuEntities db = new ThuongMaiDienTuEntities();
        [HttpGet]
        public SanPham GetSanPham(String MaSP)
        {
            return db.SanPhams.Single(s => s.MaSP.Equals(MaSP));
        }
        public List<SanPham>  GetListSanPham(string MaCH)
        {
            return db.SanPhams.Where(s => s.CuaHang.MaCuaHang.Equals(MaCH)).ToList();
        }
        public List<SanPham> GetListLoaiCTSanPham(string MaCT)
        {
            return db.SanPhams.Where(s => s.MaChiTietLoai.Equals(MaCT)).ToList();
        }
    }
}
