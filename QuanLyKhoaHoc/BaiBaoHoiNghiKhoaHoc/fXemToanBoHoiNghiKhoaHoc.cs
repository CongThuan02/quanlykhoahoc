using QuanLyKhoaHoc.BaiBaoHoiNghiKhoaHoc.CanBo;
using QuanLyKhoaHoc.BaiBaoHoiNghiKhoaHoc.SinhVien;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKhoaHoc.BaiBaoHoiNghiKhoaHoc
{
    public partial class fXemToanBoHoiNghiKhoaHoc : Form
    {
        public fXemToanBoHoiNghiKhoaHoc()
        {
            InitializeComponent();
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

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

        }
        private void fXemToanBoHoiNghiKhoaHoc_Load(object sender, EventArgs e)
        {

        }
    }
}
