using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThuongMaiDienTu.Models
{
    public class ChiMucThongKe
    {
        string cotMoc;
        int? luotTruyCap = null;
        long? sanPham = null;
        long? cuaHang = null;

        public ChiMucThongKe(string cotMoc, int? luotTruyCap, long? sanPham, long? cuaHang)
        {
            this.CotMoc = cotMoc;
            this.LuotTruyCap = luotTruyCap;
            this.SanPham = sanPham;
            this.CuaHang = cuaHang;
        }

        public string CotMoc { get => cotMoc; set => cotMoc = value; }
        public int? LuotTruyCap { get => luotTruyCap; set => luotTruyCap = value; }
        public long? SanPham { get => sanPham; set => sanPham = value; }
        public long? CuaHang { get => cuaHang; set => cuaHang = value; }        
    }
}