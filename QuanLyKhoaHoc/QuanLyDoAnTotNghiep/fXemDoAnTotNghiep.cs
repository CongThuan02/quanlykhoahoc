using QuanLyKhoaHoc.DbConnect;
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

namespace QuanLyKhoaHoc.QuanLyDoAnTotNghiep
{
    public partial class fXemDoAnTotNghiep : Form
    {
        QuanLyKhoaHocEntities _context = new QuanLyKhoaHocEntities();
        public fXemDoAnTotNghiep()
        {
            InitializeComponent();
        }
        private async Task loadingdata()
        {
            var sql = from doAnTotNghiep in _context.DoAnTotNghieps
                      join giangVien in _context.GiangViens

                      on doAnTotNghiep.GiangVienHuongDanId equals giangVien.Id

                      join sinhVien in _context.SinhViens
                      on doAnTotNghiep.SinhVienId equals sinhVien.Id



                      where doAnTotNghiep.TenDeTai.Contains(txtTimKiem.Text)
                      select new
                      {
                          maGiangVien = giangVien.TaiKhoan.MaTaiKhoan,
                          tenGiangVien = giangVien.TaiKhoan.HoTen,
                          maSinhVien = sinhVien.TaiKhoan.MaTaiKhoan,
                          tenSv = sinhVien.TaiKhoan.HoTen,
                          maDeTai = doAnTotNghiep.MaDeTai,
                          tenDeTai = doAnTotNghiep.TenDeTai,
                          moTa = doAnTotNghiep.MoTa,
                          namHoc = doAnTotNghiep.NamHoc,
                          hocKy = doAnTotNghiep.HocKy

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
                deTai.SubItems.Add(item.namHoc.ToString());
                deTai.SubItems.Add(item.hocKy.ToString());
          

                lsDoAnTotNghiep.Items.Add(deTai);
                index++;
            }
        }
      
        private async void fXemDoAnTotNghiep_Load(object sender, EventArgs e)
        {
            await loadingdata();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fChucNang back = new fChucNang();
            this.Hide();
            back.ShowDialog();  
            
        }

        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            await loadingdata();
        }

        private void lsDoAnTotNghiep_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
