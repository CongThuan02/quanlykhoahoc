using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKhoaHoc
{
    public partial class fDangNhap : Form
    {
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

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            fChucNang _fchucNang = new fChucNang();
            this.Hide();
            _fchucNang.ShowDialog();
        }
    }
}
