//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ThuongMaiDienTu.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DanhGia
    {
        public string NoiDung { get; set; }
        public string ID { get; set; }
        public string MaSP { get; set; }
        public int MucDanhGia { get; set; }
        public System.DateTime NgayDanhGia { get; set; }
        public bool AnDanh { get; set; }
    
        public virtual NguoiDung NguoiDung { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
