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
    
    public partial class GiangVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GiangVien()
        {
            this.NghienCuuKhoaHocCanBoes = new HashSet<NghienCuuKhoaHocCanBo>();
            this.NghienCuuKhoaHocSinhViens = new HashSet<NghienCuuKhoaHocSinhVien>();
        }
    
        public int Id { get; set; }
        public int TaiKhoanId { get; set; }
        public string ChucVu { get; set; }
        public string HocHam { get; set; }
        public string HocVi { get; set; }
        public string BoMon { get; set; }
    
        public virtual TaiKhoan TaiKhoan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NghienCuuKhoaHocCanBo> NghienCuuKhoaHocCanBoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NghienCuuKhoaHocSinhVien> NghienCuuKhoaHocSinhViens { get; set; }
    }
}