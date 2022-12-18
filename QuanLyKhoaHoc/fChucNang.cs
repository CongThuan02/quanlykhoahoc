using QuanLyKhoaHoc.BaiBaoHoiNghiKhoaHoc;
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

namespace QuanLyKhoaHoc
{
    public partial class fChucNang : Form
    {
        public fChucNang()
        {
            InitializeComponent();
        }

        private void menuDangXuat_Click(object sender, EventArgs e)
        {
            fDangNhap _fdangNhap = new fDangNhap();
            DialogResult dg = MessageBox.Show("Bạn có muốn đăng xuất không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                this.Hide();
                _fdangNhap.ShowDialog();
            }
           
        }

        private void menuThoatChuongTrinh_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có muốn thoát khỏi chương trình không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnXemBaiBaoKhoaHoc_Click(object sender, EventArgs e)
        {
            fXemToanBoHoiNghiKhoaHoc _toanBoHoiNghiKhoaHoc = new fXemToanBoHoiNghiKhoaHoc();
            this.Hide();
            _toanBoHoiNghiKhoaHoc.ShowDialog();
        }

        private void btnQuanLyHoiNghiCanBo_Click(object sender, EventArgs e)
        {
            fThemBaiBaoCanBo _hienthi= new fThemBaiBaoCanBo();
            this.Hide();
            _hienthi.ShowDialog();
        }

        private void btnQuanLyHoiNGhiSinhVien_Click(object sender, EventArgs e)
        {
            fThemBaiBaoChoSinhVien _them = new fThemBaiBaoChoSinhVien();
            this.Hide();
            _them.ShowDialog();
        }
    }
}
