using System;
using QuanLyKhoaHoc.DbConnect;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace QuanLyKhoaHoc.BaiBaoTrenTapChi
{
    public partial class fXemToanBoBaiBaoTrenTapChi : Form
    {
        public fXemToanBoBaiBaoTrenTapChi()
        {
            InitializeComponent();
        }
        private readonly QuanLyKhoaHocEntities _context = new QuanLyKhoaHocEntities();
        private async Task LoadingData()
        {
            var sql = from baibaotapchi in _context.BaiBaos
                      join taiKhoan in _context.TaiKhoans
                      on baibaotapchi.TaiKhoanId equals taiKhoan.Id
                      where baibaotapchi.TenBaiBao.Contains(txtTimKiem.Text) 
                      select new
                      {
                          MSV = taiKhoan.MaTaiKhoan,
                          tenSinhVien = taiKhoan.HoTen,
                          maBaiBao = baibaotapchi.MaBaiBao,
                          tenBaiBao = baibaotapchi.TenBaiBao,
                          noiDungBaiBao = baibaotapchi.MoTa,
                          namBaiBao = baibaotapchi.Nam,
                          quocGia = baibaotapchi.QuocGia,
                          loaiTaiKhoan = taiKhoan.LoaiTaiKhoan
                      };
            var data = await sql.ToListAsync();
            lsTapChi.Items.Clear();
            int index = 1;
            foreach (var item in data)
            {
                var baiBao = new ListViewItem(index.ToString());
                baiBao.SubItems.Add(item.MSV);
                baiBao.SubItems.Add(item.tenSinhVien);
                baiBao.SubItems.Add(item.maBaiBao);
                baiBao.SubItems.Add(item.tenBaiBao);
                baiBao.SubItems.Add(item.noiDungBaiBao);
                baiBao.SubItems.Add(item.quocGia);
                baiBao.SubItems.Add(item.namBaiBao.ToString());
                if(item.loaiTaiKhoan==0)
                {
                    baiBao.SubItems.Add("Giảng Viên");
                }
                else
                {
                    baiBao.SubItems.Add("Sinh Viên");
                }
                
                lsTapChi.Items.Add(baiBao);
                index++;

            }
        }

        private async void fXemToanBoBaiBaoTrenTapChi_Load(object sender, EventArgs e)
        {
            await LoadingData();
        }

        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            await LoadingData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fChucNang  back = new fChucNang();
            this.Hide();
            back.ShowDialog();
        }
    }
}
