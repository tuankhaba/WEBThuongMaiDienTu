using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThuongMaiDienTu.Models
{
    public class ChiTietDonHangTam
    {               

        public ChiTietDonHangTam(string maSP, string tenSP, string maCTKichCo, string kichCo, string tenPhanLoai, int gia, int soLuong)
        {
            this.MaSP = maSP;
            this.TenSP = tenSP;
            this.MaCTKichCo = maCTKichCo;
            this.KichCo = kichCo;
            this.TenPhanLoai = tenPhanLoai;
            this.Gia = gia;
            this.SoLuong = soLuong;
        }

        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string MaCTKichCo { get; set; }
        public string KichCo { get; set; }
        public string TenPhanLoai { get; set; }
        public int Gia { get; set; }
        public int SoLuong { get; set; }
    }
}