using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Models
{
    public class DataThongKe
    {
        DateTime batDau;
        DateTime ketThuc;
        string loaiMoc;
        List<ChiMucThongKe> datas;

        public DataThongKe(DateTime batDau, DateTime ketThuc, List<ChiMucThongKe> datas)
        {
            this.BatDau = batDau;
            this.KetThuc = ketThuc;
            this.Datas = datas;
        }
        public string HashValue(long? value)
        {
            return value.HasValue ? value.Value.ToString() : "null";
        }
        public DateTime BatDau { get => batDau; set => batDau = value; }
        public DateTime KetThuc { get => ketThuc; set => ketThuc = value; }
        public List<ChiMucThongKe> Datas { get => datas; set => datas = value; }
        public string LoaiMoc { get => loaiMoc; set => loaiMoc = value; }
        public List<string> XuatDinhDangDoThi()
        {
            List<string> ListDTstring = new List<string>();
            foreach (var item in this.Datas)
            {
                string mocmoi = LoaiMoc + ": '" + item.CotMoc + "', 'Lượt truy cập': " + HashValue(item.LuotTruyCap) + ", 'Sản phẩm': " + HashValue(item.SanPham) + " , 'Cửa hàng': " + HashValue(item.CuaHang);
                ListDTstring.Add(mocmoi);
            }
            return ListDTstring;
        }
        public DataThongKe(CuaHang Ch, SanPham a, DateTime ngayBatDau, DateTime NgayKetThuc)
        {
            this.BatDau = ngayBatDau.Date;
            this.KetThuc = NgayKetThuc.Date;
            int SoNgay = (NgayKetThuc - ngayBatDau).Days;
            ThuongMaiDienTuEntities db = new ThuongMaiDienTuEntities();
            List<DonHang> DHs = db.DonHangs.Where(d => d.MaCuaHang.Equals(Ch.MaCuaHang)).ToList();
            List<ChiTietDonHang> ctdhs = null;
            if (a != null)
                ctdhs = db.ChiTietDonHangs.Where(ct => ct.ChiTietKichCo.ChiTietPhanLoai.SanPham.MaSP.Contains(a.MaSP)).ToList();
            Datas = new List<ChiMucThongKe>();
            int buocnhay;
            if (SoNgay < 31)
            {
                buocnhay = 1;
                LoaiMoc = "Ngay";
            }
            else if (SoNgay < 100)
            {
                buocnhay = 10;
                LoaiMoc = "Ngays";
            }
            else if (SoNgay < 366)
            {
                buocnhay = 30;
                LoaiMoc = "Thang";
            }
            else if (SoNgay < 1096)
            {
                buocnhay = 100;
                LoaiMoc = "Ngayss";
            }
            else
            {
                buocnhay = 365;
                LoaiMoc = "Nam";
            }
            for (int i = 0; i < SoNgay; i += buocnhay)
            {
                DateTime moc = BatDau.AddDays(i);
                string cotmoc = string.Format("{0:dd/MM/yyyy}", moc);
                long cuahang = DHs.Where(d => d.NgayMua.Date == BatDau.Date).Sum(d => d.ThanhTien);
                int? sanpham = null;
                if (a != null)
                    sanpham = ctdhs.Where(d => d.DonHang.NgayMua.Date == BatDau.Date && d.MaSP.Equals(a.MaSP)).Sum(d => d.TongTien);
                Datas.Add(new ChiMucThongKe(cotmoc, null, sanpham, cuahang));
            }      
                DateTime mocK = ketThuc.Date;
                string cotmocK = string.Format("{0:dd/MM/yyyy}", mocK);
                long cuahangK = DHs.Where(d => d.NgayMua.Date == ketThuc.Date).Sum(d => d.ThanhTien);
                int? sanphamK = null;
                if (a != null)            
                    sanphamK = ctdhs.Where(d => d.DonHang.NgayMua.Date == KetThuc.Date && d.MaSP.Equals(a.MaSP)).Sum(d => d.TongTien);
                Datas.Add(new ChiMucThongKe(cotmocK, null, sanphamK, cuahangK));
        } 
    }
}