using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Controllers
{
    public class DonHangApiController : ApiController
    {
        ThuongMaiDienTuEntities db = new ThuongMaiDienTuEntities();
        public DonHang GetDonHang(string maDH)
        {
            return db.DonHangs.Single(d => d.MaDonHang.Equals(maDH));
        }
        public ChiTietDonHang GetChiTietDonHang(string maCT)
        {
            return db.ChiTietDonHangs.Single(ct => ct.MaCTKichCo.Equals(maCT));
        }

    }
}
