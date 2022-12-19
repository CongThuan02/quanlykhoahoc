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

namespace QuanLyKhoaHoc
{
    
    public partial class fDangNhap : Form
    {

        private readonly QuanLyKhoaHocEntities _context = new QuanLyKhoaHocEntities();
        public fDangNhap()
        {
            InitializeComponent();
        }

      

        private void ckHienThiMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if (ckHienThiMatKhau.Checked)
            {
                txtMatKhau.PasswordChar = (char)0;
            }
            else
            {
                txtMatKhau.PasswordChar = '*';
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtMatKhau.PasswordChar = '*';
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có muốn thoát không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                Application.Exit(); 
            }
        }

        private async void btnDangNhap_Click(object sender, EventArgs e)
        {
            //string taiKhoan = txtTaiKhoan.Text;
            //string matKhau = txtMatKhau.Text;
            //var check =await _context.TaiKhoans.Where(x=> x.MaTaiKhoan.Equals(taiKhoan) && x.MatKhau.Equals(matKhau)).AnyAsync();
            //if (check)
            //{
                MessageBox.Show("Đăng nhập thành Công");
                fChucNang _fchucNang = new fChucNang();
                this.Hide();
                _fchucNang.ShowDialog();
          
        }
    }
}
