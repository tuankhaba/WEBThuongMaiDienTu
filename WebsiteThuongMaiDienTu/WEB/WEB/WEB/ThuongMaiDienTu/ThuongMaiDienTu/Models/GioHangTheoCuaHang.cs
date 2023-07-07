using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Models
{
    public class GioHangTheoCuaHang
    {
        CuaHang cH;
        List<ChiTietGioHang> cTGHs;
        public CuaHang CH { get => cH; set => cH = value; }
        public List<ChiTietGioHang> CTGHs { get => cTGHs; set => cTGHs = value; }
        public GioHangTheoCuaHang(CuaHang CH, List<ChiTietGioHang> CTGHs)
        {
            this.CH = CH;
            this.CTGHs = CTGHs;
        }
    }
}