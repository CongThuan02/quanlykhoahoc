using QuanLyKhoaHoc.BaiBaoHoiNghiKhoaHoc.CanBo;
using QuanLyKhoaHoc.BaiBaoHoiNghiKhoaHoc.SinhVien;
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

namespace QuanLyKhoaHoc.BaiBaoHoiNghiKhoaHoc
{
    public partial class fXemToanBoHoiNghiKhoaHoc : Form
    {
        private readonly QuanLyKhoaHocEntities _context = new QuanLyKhoaHocEntities();
        public fXemToanBoHoiNghiKhoaHoc()
        {
            InitializeComponent();
        }
        private async Task LoadingData()
        {

            {
                var sql = from hoiNghiKhoaHoc in _context.HoiNghiKhoaHocs
                          join taiKhoan in _context.TaiKhoans
                          on hoiNghiKhoaHoc.NguoiSoHuuId equals taiKhoan.Id
                          where hoiNghiKhoaHoc.TenBaiBao.Contains(txtTimKiem.Text) 
                          select new
                          {
                              MSV = taiKhoan.MaTaiKhoan,
                              TenSinhVien = taiKhoan.HoTen,
                              MaBaiBao = hoiNghiKhoaHoc.MaBaiBao,
                              TenBaiBao = hoiNghiKhoaHoc.TenBaiBao,
                              NoiDungBaiBao = hoiNghiKhoaHoc.MoTa,
                              Nam = hoiNghiKhoaHoc.Nam,
                          };
                var data = await sql.ToListAsync();
                lsDanhSach.Items.Clear();
                int index = 1;
                foreach (var item in data)
                {
                    var baiBao = new ListViewItem(index.ToString());
                    baiBao.SubItems.Add(item.MSV);
                    baiBao.SubItems.Add(item.TenSinhVien);
                    baiBao.SubItems.Add(item.MaBaiBao);
                    baiBao.SubItems.Add(item.TenBaiBao);
                    baiBao.SubItems.Add(item.NoiDungBaiBao);
                    baiBao.SubItems.Add(item.Nam.ToString());

                    lsDanhSach.Items.Add(baiBao);
                    index++;
                }
            }
        }




        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fChucNang _chucNang = new fChucNang();
            this.Hide();
            _chucNang.ShowDialog();
        }

        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            await LoadingData();
        }
        private async void fXemToanBoHoiNghiKhoaHoc_Load(object sender, EventArgs e)
        {
            await LoadingData();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

     
    }
}
