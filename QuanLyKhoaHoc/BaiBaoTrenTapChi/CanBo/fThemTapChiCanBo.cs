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

namespace QuanLyKhoaHoc.BaiBaoTrenTapChi.CanBo
{
    public partial class fThemTapChiCanBo : Form
    {
    private readonly QuanLyKhoaHocEntities _context = new QuanLyKhoaHocEntities();
    private async     Task
LoadingData()
        {
            var sql = from baibaotapchi in _context.BaiBaos
                      join taiKhoan in _context.TaiKhoans
                      on baibaotapchi.TaiKhoanId equals taiKhoan.Id
                      where baibaotapchi.TenBaiBao.Contains(txtTimKiem.Text) && taiKhoan.LoaiTaiKhoan == 0
                      select new
                      {
                          maCanBo = taiKhoan.MaTaiKhoan,
                          tenGiangVien = taiKhoan.HoTen,
                          maBaiBao = baibaotapchi.MaBaiBao,
                          tenBaiBao = baibaotapchi.TenBaiBao,
                          noiDungBaiBao = baibaotapchi.MoTa,
                          namBaiBao= baibaotapchi.Nam,
                          quocGia = baibaotapchi.QuocGia,
                      };
            var data = await sql.ToListAsync();
            lsBaiBaoCanBo.Items.Clear();
            int index = 1;
            foreach(var item in data)
            {
                var baiBao = new ListViewItem(index.ToString());
                baiBao.SubItems.Add(item.maCanBo);
                baiBao.SubItems.Add(item.tenGiangVien);
                baiBao.SubItems.Add(item.maBaiBao);
                baiBao.SubItems.Add(item.tenBaiBao);
                baiBao.SubItems.Add(item.noiDungBaiBao);
                baiBao.SubItems.Add(item.namBaiBao.ToString());
                baiBao.SubItems.Add(item.quocGia);
                lsBaiBaoCanBo.Items.Add(baiBao);
                index++;

            }
        }


        private async void fThemTapChiCanBo_Load(object sender, EventArgs e)
        {
            await LoadingData();
            lsBaiBaoCanBo.FullRowSelect= true;
            txtTenGiangVien.Enabled= false;
            cbGiangVien.Items.Clear();
            var canBo= await _context.GiangViens.ToListAsync();
            foreach(var cb in canBo)
            {
                var item = new
                {
                    maCanBo = cb.TaiKhoan.MaTaiKhoan,
                    id =cb.Id,
                    hoTen = cb.TaiKhoan.HoTen,
                };
                cbGiangVien.Items.Add(item);
            }
            cbGiangVien.ValueMember = "maCanBo";

        }

        private void cbGiangVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tenCanBo = cbGiangVien.SelectedItem;
            txtTenGiangVien.Text = tenCanBo.GetType()
                .GetProperty("hoTen")
                .GetValue(tenCanBo, null)
                .ToString();
        }


        

































































        public fThemTapChiCanBo()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {

        }
    }
}
