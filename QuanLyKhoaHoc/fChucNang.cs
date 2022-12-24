using QuanLyKhoaHoc.BaiBaoHoiNghiKhoaHoc;
using QuanLyKhoaHoc.BaiBaoHoiNghiKhoaHoc.CanBo;
using QuanLyKhoaHoc.BaiBaoHoiNghiKhoaHoc.SinhVien;
using QuanLyKhoaHoc.QuanLyDoAnTotNghiep.CanBo;
using QuanLyKhoaHoc.BaiBaoTrenTapChi;
using QuanLyKhoaHoc.BaiBaoTrenTapChi.CanBo;
using QuanLyKhoaHoc.BaiBaoTrenTapChi.SinhVien;
using QuanLyKhoaHoc.QuanLyNhienCuuKhoaHoc;
using QuanLyKhoaHoc.QuanLyNhienCuuKhoaHoc.CanBo;
using QuanLyKhoaHoc.QuanLyNhienCuuKhoaHoc.SinhVien;
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

        private void btnXemToanBoTapChi_Click(object sender, EventArgs e)
        {
        fXemToanBoBaiBaoTrenTapChi _tapChi = new fXemToanBoBaiBaoTrenTapChi();
            this.Hide();
            _tapChi.ShowDialog();
        }

        private void btnTapChiGiaoVien_Click(object sender, EventArgs e)
        {
            fThemTapChiCanBo _fThemTapChiCanBo = new fThemTapChiCanBo();
            this.Hide();
            _fThemTapChiCanBo.ShowDialog();
        }

        private void btnTapChiSinhVien_Click(object sender, EventArgs e)
        {
            fThemTapChiSinhVien _themTapChiSinhVien = new fThemTapChiSinhVien();
            this.Hide();
            _themTapChiSinhVien.ShowDialog();
        }

        private void btnDeTaiNhienCuuKhoaHoc_Click(object sender, EventArgs e)
        {
            fQuanLyNhienCuuKhoaHoc _views = new fQuanLyNhienCuuKhoaHoc();
            this.Hide();
            _views.ShowDialog();
        }

        private void btnDanhSachNhienCuuKhoaHocGiaoVien_Click(object sender, EventArgs e)
        {
            fQuanLyCanBo _view = new fQuanLyCanBo();    
            this.Hide();
            _view.ShowDialog();
        }

        private void btnDanhSachSinhVienThamGiaNhienCuu_Click(object sender, EventArgs e)
        {
            fQuanLySinhVien _view = new fQuanLySinhVien();
            this.Hide();    
            _view.ShowDialog();
        }

        private void btnDanhSachGiangVienHuongDan_Click(object sender, EventArgs e)
        {
            fGiangVienHD show = new fGiangVienHD();
            this.Hide();
            show.ShowDialog();
        }
    }
}
