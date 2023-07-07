using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThuongMaiDienTu.Models;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;

namespace ThuongMaiDienTu.Models
{
    public class HoTro
    {
        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
            "đ",
            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
            "í","ì","ỉ","ĩ","ị",
            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
            "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
            "d",
            "e","e","e","e","e","e","e","e","e","e","e",
            "i","i","i","i","i",
            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
            "u","u","u","u","u","u","u","u","u","u","u",
            "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }
        public static string LayDiaChi(string maXP)
        {
            Xa_Phuong a = (new ThuongMaiDienTuEntities()).Xa_Phuong.Single(x => x.MaXP.Equals(maXP));
            return a.TenXa + " - " + a.Quan_Huyen.TenHuyen + " - " + a.Quan_Huyen.Tinh_ThanhPho.TenTinh;
        }
        public static bool IsEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public static string HashToSHA512(string s)
        {
            return Convert.ToBase64String(SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes(s)));
        }
        public static int LayTongSoLuongTonKho(SanPham a)
        {
            return (new ThuongMaiDienTuEntities().ChiTietKichCoes.Where(t => t.ChiTietPhanLoai.MaSP.Equals(a.MaSP)).ToList().Sum(s => s.SoLuong));
        }
        public static string LayKhoanGia(SanPham a)
        {
            try
            {
                List<ChiTietPhanLoai> ct = a.ChiTietPhanLoais.Where(p => p.MaSP == a.MaSP).ToList();
                string min = string.Format("{0:0,0}", ct.Min(m => m.Gia));
                string max = string.Format("{0:0,0}", ct.Max(m => m.Gia));
                if (min.Equals(max))
                    return min + "VNĐ";
                return min + " - " + max + "VNĐ";
            }
            catch
            {
                return 0.ToString();
            }
        }
        public static string LayGiaMin(SanPham a)
        {
            try
            {
                List<ChiTietPhanLoai> ct = a.ChiTietPhanLoais.Where(p => p.MaSP == a.MaSP).ToList();
                int gia = ct.Min(m => m.Gia);
                if (gia < 10)
                    return gia.ToString();
                return string.Format("{0:0,0}", gia);
            }
            catch
            {
                return 0.ToString();
            }
        }
        public static string TinhTienGiaGiam(SanPham a)
        {
            try
            {
                List<ChiTietPhanLoai> ct = a.ChiTietPhanLoais.Where(p => p.MaSP == a.MaSP).ToList();
                int min = ct.Min(m => m.Gia);
                int gg = Convert.ToInt32(min - a.GiamGia / 100 * min);
                if (gg < 10)
                    return gg.ToString();
                return string.Format("{0:0,0}", gg);
            }
            catch
            {
                return 0.ToString();
            }
        }
        public static int LaySoNguoiDungMaKM(KhuyenMai m)
        {
            ThuongMaiDienTuEntities db = new ThuongMaiDienTuEntities();
            return db.DonHangs.Count(h => h.MaVoucher.Equals(m.MaVoucher) && h.MaCuaHang.Equals(m.MaCuaHang));
        }
        public static string TrangThaiKhuyenMai(KhuyenMai m)
        {
            DateTime today = DateTime.Now;
            if (m.NgayBatDau > today)
                return "Chưa diễn ra";
            else if (m.NgayKetThuc < today)
                return "Đã kết thúc";
            return "Đang diễn ra";

        }
        public static int LayDoanhSo(SanPham a)
        {
            ThuongMaiDienTuEntities db = new ThuongMaiDienTuEntities();
            return db.ChiTietDonHangs.Where(t => t.ChiTietKichCo.ChiTietPhanLoai.MaSP.Equals(a.MaSP)).ToList().Sum(s => s.SoLuong);
        }
        public static string XuatDoanhSo(SanPham a)
        {
            int ds = LayDoanhSo(a);
            if (ds < 1000)
                return ds.ToString();
            return (ds / 1000).ToString() + "k";
        }
        public static string XuatGiaTheoK(long gia)
        {
            if (gia < 1000)
                return gia.ToString()+"đ";
            return (gia / 1000).ToString() + "k";
        }
        public static double LayDanhGiaShop(CuaHang a)
        {
            double TongDG = 0;
            int TongSPDG = 0;
            foreach( var item in a.SanPhams)
            {
                if(item.SoLuongDanhGia>0)
                {
                    TongDG += item.DanhGia;
                    TongSPDG++;
                }    
            }
            return TongSPDG == 0 ? 0 : TongDG / TongSPDG;
        }
        public static List<GioHangTheoCuaHang> LayDSGioHangChiaTheoCuaHang(List<ChiTietGioHang> CtGHs)
        {
            List<GioHangTheoCuaHang> GH_chs = new List<GioHangTheoCuaHang>();
            ThuongMaiDienTuEntities db = new ThuongMaiDienTuEntities();
            CtGHs.ForEach(item => item.ChiTietKichCo = db.ChiTietKichCoes.Single(ct => ct.MaCTKichCo.Equals(item.MaCTKichCo)));           
            foreach (ChiTietGioHang item in CtGHs)
            {
                CuaHang cuahg;
                try
                {
                    cuahg = item.ChiTietKichCo.ChiTietPhanLoai.SanPham.CuaHang;
                }
                catch
                {
                    cuahg = db.ChiTietKichCoes.Single(ct => ct.MaCTKichCo.Equals(item.MaCTKichCo)).ChiTietPhanLoai.SanPham.CuaHang;
                }
                if (GH_chs.Count(gh => gh.CH.MaCuaHang.Equals(cuahg.MaCuaHang)) == 0)
                    GH_chs.Add(new GioHangTheoCuaHang(cuahg, CtGHs.Where(gh => gh.ChiTietKichCo.ChiTietPhanLoai.SanPham.MaCuaHang.Equals(cuahg.MaCuaHang)).ToList()));
            }
            db.SaveChanges();
            return GH_chs;
        }
        public static int TinhTongTien(List<ChiTietDonHangTam> CTDHs)
        {
            ThuongMaiDienTuEntities db = new ThuongMaiDienTuEntities();
            int tong = 0;
            foreach(ChiTietDonHangTam item in CTDHs)
            {
                ChiTietKichCo a = db.ChiTietKichCoes.Single(k => k.MaCTKichCo.Equals(item.MaCTKichCo));
                if (a.ChiTietPhanLoai.SanPham.GiamGia > 0 && a.ChiTietPhanLoai.SanPham.ThoiHan >= DateTime.Now)
                {
                    int gia = Convert.ToInt32(item.Gia * 1.0 / 100 * a.ChiTietPhanLoai.SanPham.GiamGia);
                    tong += gia * item.SoLuong;
                }
                else
                    tong += a.ChiTietPhanLoai.Gia * item.SoLuong;                
            }
            return tong;
        }
        public static int TinhTongTien(List<ChiTietDonHang> CTDH)
        {
            int tong = 0;
            foreach (ChiTietDonHang item in CTDH)
            {
                if (item.ChiTietKichCo.ChiTietPhanLoai.SanPham.GiamGia > 0 && item.ChiTietKichCo.ChiTietPhanLoai.SanPham.ThoiHan >= DateTime.Now)
                {
                    int gia = item.ChiTietKichCo.ChiTietPhanLoai.Gia - Convert.ToInt32(item.ChiTietKichCo.ChiTietPhanLoai.Gia * 1.0 / 100 * item.ChiTietKichCo.ChiTietPhanLoai.SanPham.GiamGia);
                    tong += gia * item.SoLuong;
                }
                else
                    tong += item.ChiTietKichCo.ChiTietPhanLoai.Gia * item.SoLuong;
            }
            return tong;
        }
        public static bool XetGiamGiaConHan(SanPham a)
        {
            DateTime now = DateTime.Now;
            if (a.GiamGia > 0 && a.ThoiHan >= now)
                return true;
            return false;
        }
        public static string LayNDCamThanDanhGia(int muc)
        {
            return muc == 5 ? "Cực kì hài lòng" : muc == 4 ? "Hài lòng" : muc == 3 ? "Bình thường" : muc == 2 ? "Không hài lòng" : "Quá tệ";
        }
        public static string ToFirstUpper(string s)
        {
            if (String.IsNullOrEmpty(s))
                return s;

            string result = "";

            //lấy danh sách các từ  

            string[] words = s.Split(' ');

            foreach (string word in words)
            {
                // từ nào là các khoảng trắng thừa thì bỏ  
                if (word.Trim() != "")
                {
                    if (word.Length > 1)
                        result += word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower() + " ";
                    else
                        result += word.ToUpper() + " ";
                }

            }
            return result.Trim();
        }        
    }
}