using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyKhoaHoc.DbConnect;

namespace QuanLyKhoaHoc.QuanLyNhienCuuKhoaHoc
{
    public partial class fXemNhienCuuKhoaHoc : Form
    {
        private readonly QuanLyKhoaHocEntities _context = new QuanLyKhoaHocEntities();
        public fXemNhienCuuKhoaHoc()
        {
            InitializeComponent();
        }



        private async Task loadingdata()
        {
            var sql = from nhienCuuKhoaHoc in _context.NghienCuuKhoaHocSinhViens
                      join giangVien in _context.GiangViens

                      on nhienCuuKhoaHoc.GiangVienHuongDanId equals giangVien.Id

                      join sinhVien in _context.SinhViens
                      on nhienCuuKhoaHoc.SinhVienId equals sinhVien.Id



                      where nhienCuuKhoaHoc.TenDeTai.Contains(txtTimKiem.Text)
                      select new
                      {
                          maGiangVien = giangVien.TaiKhoan.MaTaiKhoan,
                          tenGiangVien = giangVien.TaiKhoan.HoTen,
                          maSinhVien = sinhVien.TaiKhoan.MaTaiKhoan,
                          tenSv = sinhVien.TaiKhoan.HoTen,
                          maDeTai = nhienCuuKhoaHoc.MaDeTai,
                          tenDeTai = nhienCuuKhoaHoc.TenDeTai,
                        
                          moTa = nhienCuuKhoaHoc.MoTa,

                      };
            var data = await sql.ToListAsync();
            lsDoAnTotNghiep.Items.Clear();
            int index = 1;
            foreach (var item in data)
            {
                var deTai = new ListViewItem(index.ToString());
                deTai.SubItems.Add(item.maGiangVien);
                deTai.SubItems.Add(item.tenGiangVien);
                deTai.SubItems.Add(item.maDeTai);
                deTai.SubItems.Add(item.tenDeTai);
                deTai.SubItems.Add(item.maSinhVien);
                deTai.SubItems.Add(item.tenSv);
                deTai.SubItems.Add(item.moTa);
               

                lsDoAnTotNghiep.Items.Add(deTai);
                index++;
            }
        }
        private void fXemNhienCuuKhoaHoc_Load(object sender, EventArgs e)
        {
            loadingdata();
        }
    }
}
