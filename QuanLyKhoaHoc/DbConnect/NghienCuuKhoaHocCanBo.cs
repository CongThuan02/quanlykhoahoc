//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyKhoaHoc.DbConnect
{
    using System;
    using System.Collections.Generic;
    
    public partial class NghienCuuKhoaHocCanBo
    {
        public int Id { get; set; }
        public string MaDeTai { get; set; }
        public string TenDeTai { get; set; }
        public string MoTa { get; set; }
        public string FileDinhKem { get; set; }
        public string GhiChu { get; set; }
        public int GiangVienDangKyId { get; set; }
        public int TrangThai { get; set; }
    
        public virtual GiangVien GiangVien { get; set; }
    }
}