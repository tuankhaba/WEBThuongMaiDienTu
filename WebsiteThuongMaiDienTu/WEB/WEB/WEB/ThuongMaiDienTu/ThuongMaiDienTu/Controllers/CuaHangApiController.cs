using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Controllers
{
    public class CuaHangApiController : ApiController
    {
        [HttpGet]
        public DataThongKe GetDSThongKeDoanhThu(CuaHang ch, SanPham sp,DateTime NgayBD, DateTime NgayKT)
        {
            return new DataThongKe(ch, sp, NgayBD, NgayKT);
        }
    }
}

