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

namespace QuanLyKhoaHoc.QuanLyDoAnTotNghiep.SinhVien
{
    public partial class fSinhVienBaoLamDoAn : Form
    {
        private readonly QuanLyKhoaHocEntities _context = new QuanLyKhoaHocEntities();
        public fSinhVienBaoLamDoAn()
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
                          namHoc = doAnTotNghiep.NamHoc,
                          hocKy = doAnTotNghiep.HocKy,
                          moTa = doAnTotNghiep.MoTa,

                      };
            var data = await sql.ToListAsync();
            lsSinhVien.Items.Clear();
            int index = 1;
            foreach (var item in data)
            {
                var deTai = new ListViewItem(index.ToString());
                deTai.SubItems.Add(item.maSinhVien);

                deTai.SubItems.Add(item.tenSv);
                deTai.SubItems.Add(item.maDeTai);
                deTai.SubItems.Add(item.tenDeTai);

                deTai.SubItems.Add(item.maGiangVien);
                deTai.SubItems.Add(item.tenGiangVien);
                deTai.SubItems.Add(item.moTa);
                deTai.SubItems.Add(item.hocKy.ToString());

                deTai.SubItems.Add(item.namHoc.ToString());

                lsSinhVien.Items.Add(deTai);
                index++;
            }
        }
        private async Task getListCanBo()
        {
            cbCanBo.Items.Clear();
            var canBos = await _context.GiangViens.ToListAsync();
            foreach (var cb in canBos)
            {
                var item = new
                {
                    maGiangVien = cb.TaiKhoan.MaTaiKhoan,
                    Id = cb.Id,
                    HoTen = cb.TaiKhoan.HoTen
                };
                cbCanBo.Items.Add(item);
            }

            cbCanBo.ValueMember = "maGiangVien";
        }
        private async Task getListSv()
        {
            cbSinhVien.Items.Clear();
            var sinhVien = await _context.SinhViens.ToListAsync();
            foreach (var sv in sinhVien)
            {
                var item = new
                {
                    maSinhVien = sv.TaiKhoan.MaTaiKhoan,
                    Id = sv.Id,
                    hoTen = sv.TaiKhoan.HoTen
                };
                cbSinhVien.Items.Add(item);
            }

            cbSinhVien.ValueMember = "maSinhVien";
        }


        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            await loadingdata();
        }

        private async void fSinhVienBaoLamDoAn_Load(object sender, EventArgs e)
        {
            await loadingdata();

            lsSinhVien.FullRowSelect = true;
            txtTenGiangVien.Enabled = false;
            txtTenSinhVien.Enabled = false;
            await getListCanBo();
            await getListSv();
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            string maDeTai = txtMaDeTai.Text.Trim();
            string tenDeTai = txtTenDeTai.Text.Trim();
            string moTa = txtMoTa.Text.Trim();
            int nam = int.Parse(txtNamHoc.Text);
            int hocKy = int.Parse(txtHocKy.Text);
            string maGiangVien = cbCanBo.SelectedItem.ToString();
            string maSinhVien = cbSinhVien.SelectedItem.ToString();
            var curentCb = cbCanBo.SelectedItem;
            var taiKhoanGiangVien = curentCb.GetType().GetProperty("maGiangVien").GetValue(curentCb, null).ToString();
            var curentSv = cbSinhVien.SelectedItem;
            var taiKhoanSinhVien = curentSv.GetType().GetProperty("maSinhVien").GetValue(curentSv, null).ToString();

            var idGiangVienHuongDan = await _context.GiangViens.Where(x => x.TaiKhoan.MaTaiKhoan.Equals(taiKhoanGiangVien)).Select(x => x.Id).FirstOrDefaultAsync();

            var idSinhVien = await _context.SinhViens.Where(x => x.TaiKhoan.MaTaiKhoan.Equals(taiKhoanSinhVien)).Select(x => x.Id).FirstOrDefaultAsync();

            var doAnTotNghiep = new DoAnTotNghiep
            {
                MaDeTai = maDeTai,
                TenDeTai = tenDeTai,
                NamHoc = nam,
                HocKy = hocKy,
                GiangVienHuongDanId = idGiangVienHuongDan,
                MoTa = moTa,
                SinhVienId = idSinhVien,
            };

            _context.DoAnTotNghieps.Add(doAnTotNghiep);
            await _context.SaveChangesAsync();
            txtTenDeTai.Clear();
            txtHocKy.Clear();
            txtMaDeTai.Clear();
            txtMoTa.Clear();
            txtNamHoc.Clear();
            txtTenGiangVien.Clear();
            MessageBox.Show("thêm thông tin thành công");
            loadingdata();
        }

        private void cbCanBo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var currentCb = cbCanBo.SelectedItem;
            txtTenGiangVien.Text = currentCb.GetType().GetProperty("HoTen").GetValue(currentCb, null).ToString();
        }

        private void cbSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            var current = cbSinhVien.SelectedItem;
            txtTenSinhVien.Text = current.GetType().GetProperty("hoTen").GetValue(current, null).ToString();
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            var maDeTai = txtMaDeTai.Text;
            var tenDeTai = txtTenDeTai.Text;
            var namHoc = txtNamHoc.Text;
            var moTa = txtMoTa.Text;
            var hocKy = txtHocKy.Text;
            var result = await _context.DoAnTotNghieps.Where(x => x.MaDeTai.Equals(maDeTai)).FirstOrDefaultAsync();
            if (result != null)
            {
                try
                {
                    result.MoTa = moTa;
                    result.TenDeTai = tenDeTai;
                    result.NamHoc = int.Parse(namHoc);
                    result.HocKy = int.Parse(hocKy);
                    result.MaDeTai = maDeTai;
                    _context.Entry(result).State = EntityState.Modified;
                    _context.SaveChanges();
                    MessageBox.Show("sửa bài báo thành công");
                    fSinhVienBaoLamDoAn_Load(sender, e);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Bạn không thể sửa mã bài báo ở đây");
            }
        }

        private async void lsSinhVien_Click(object sender, EventArgs e)
        {
            if (lsSinhVien.SelectedItems.Count == 0)
                return;

            ListViewItem item = lsSinhVien.SelectedItems[0];

            var maGV = item.SubItems[5].Text;
            var giangVien = await _context.GiangViens.ToListAsync();
            cbCanBo.SelectedIndex = giangVien.FindIndex(x => x.TaiKhoan.MaTaiKhoan.Equals(maGV));
            var maSv = item.SubItems[1].Text;
            var sinhVien = await _context.SinhViens.ToListAsync();
            cbSinhVien.SelectedIndex = sinhVien.FindIndex(x => x.TaiKhoan.MaTaiKhoan.Equals(maSv));
            txtMaDeTai.Text = lsSinhVien.SelectedItems[0].SubItems[3].Text;
            txtTenDeTai.Text = lsSinhVien.SelectedItems[0].SubItems[4].Text;
            txtMoTa.Text = lsSinhVien.SelectedItems[0].SubItems[7].Text;
            txtHocKy.Text = lsSinhVien.SelectedItems[0].SubItems[8].Text;
            txtNamHoc.Text = lsSinhVien.SelectedItems[0].SubItems[9].Text;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var maDeTai = lsSinhVien.SelectedItems[0].SubItems[3].Text;
            var xoaDoAnTotNghiep = _context.DoAnTotNghieps.FirstOrDefault(c => c.MaDeTai.Equals(maDeTai));
            if (xoaDoAnTotNghiep != null)
            {
                _context.DoAnTotNghieps.Remove(xoaDoAnTotNghiep);
                _context.SaveChanges();
                MessageBox.Show("xóa bài báo thành công");
                fSinhVienBaoLamDoAn_Load(sender, e);
            }
            else
            {
                MessageBox.Show("bài báo không tồn tại");
            }
        }
    }
}
