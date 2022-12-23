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

namespace QuanLyKhoaHoc.BaiBaoTrenTapChi.SinhVien
{
    public partial class fThemTapChiSinhVien : Form
    {
        private readonly QuanLyKhoaHocEntities _context = new QuanLyKhoaHocEntities();
        public fThemTapChiSinhVien()
        {
            InitializeComponent();
        }
        private async Task LoadingData()
        {
            var sql = from baibaotapchi in _context.BaiBaos
                      join taiKhoan in _context.TaiKhoans
                      on baibaotapchi.TaiKhoanId equals taiKhoan.Id
                      where baibaotapchi.TenBaiBao.Contains(txtTimKiem.Text) && taiKhoan.LoaiTaiKhoan == 1
                      select new
                      {
                          MSV = taiKhoan.MaTaiKhoan,
                          tenSinhVien = taiKhoan.HoTen,
                          maBaiBao = baibaotapchi.MaBaiBao,
                          tenBaiBao = baibaotapchi.TenBaiBao,
                          noiDungBaiBao = baibaotapchi.MoTa,
                          namBaiBao = baibaotapchi.Nam,
                          quocGia = baibaotapchi.QuocGia,
                      };
            var data = await sql.ToListAsync();
            lsBaiBaoSV.Items.Clear();
            int index = 1;
            foreach (var item in data)
            {
                var baiBao = new ListViewItem(index.ToString());
                baiBao.SubItems.Add(item.MSV);
                baiBao.SubItems.Add(item.tenSinhVien);
                baiBao.SubItems.Add(item.maBaiBao);
                baiBao.SubItems.Add(item.tenBaiBao);
                baiBao.SubItems.Add(item.noiDungBaiBao);
                baiBao.SubItems.Add(item.namBaiBao.ToString());
                baiBao.SubItems.Add(item.quocGia);
                lsBaiBaoSV.Items.Add(baiBao);
                index++;

            }
        }
        private async void fThemTapChiSinhVien_Load(object sender, EventArgs e)
        {
            await LoadingData();
            lsBaiBaoSV.FullRowSelect = true;
            txtTenSV.Enabled = false;
            cbSinhVien.Items.Clear();
            var canBo = await _context.SinhViens.ToListAsync();
            foreach (var cb in canBo)
            {
                var item = new
                {
                    maCanBo = cb.TaiKhoan.MaTaiKhoan,
                    id = cb.Id,
                    hoTen = cb.TaiKhoan.HoTen,
                };
                cbSinhVien.Items.Add(item);
            }
            cbSinhVien.ValueMember = "maCanBo";
        }
        private void cbSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tenCanBo = cbSinhVien.SelectedItem;
            txtTenSV.Text = tenCanBo.GetType()
                .GetProperty("hoTen")
                .GetValue(tenCanBo, null)
                .ToString();
        }
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            string maBaiBao = txtMaBaiBao.Text.Trim();
            string tenBaiBao = txtTenBaiBao.Text.Trim();
            string moTa = txtMoTa.Text.Trim();
            string nam = txtNam.Text.Trim();
            string quocGia = txtQuocGia.Text.Trim();
            var current = cbSinhVien.SelectedItem;
            var maTaiKhoan = current.GetType().GetProperty("maCanBo").GetValue(current, null).ToString();
            //MessageBox.Show(maTaiKhoan);
            var id = await _context.TaiKhoans.Where(x => x.MaTaiKhoan.Equals(maTaiKhoan)).Select(x => x.Id).FirstOrDefaultAsync();
            var idBaiBao = await _context.BaiBaos.Where(x => x.MaBaiBao.Equals(maBaiBao)).Select(x => x.MaBaiBao).FirstOrDefaultAsync();
            if (idBaiBao != null)
            {
                txtMaBaiBao.Clear();
                txtMoTa.Clear();
                txtTenBaiBao.Clear();
                txtMoTa.Clear();
                txtQuocGia.Clear();
                txtNam.Clear();
                MessageBox.Show("Mã bài báo đã tồn tại");
            }
            else
            {
                if (maBaiBao == "" || tenBaiBao == "" || moTa == "" || quocGia == "" || nam == "")
                {
                    MessageBox.Show("vui lòng nhập đầy đủ thông tin cho bài báo");
                }
                else
                {
                    var baibao = new BaiBao
                    {
                        MaBaiBao = maBaiBao,
                        TenBaiBao = tenBaiBao,
                        TaiKhoanId = id,
                        MoTa = moTa,
                        Nam = int.Parse(nam),
                        QuocGia = quocGia,

                    };
                    _context.BaiBaos.Add(baibao);
                    await _context.SaveChangesAsync();
                    MessageBox.Show("Thêm bài báo thành công");
                    txtMaBaiBao.Clear();
                    txtMoTa.Clear();
                    txtTenBaiBao.Clear();
                    fThemTapChiSinhVien_Load(sender, e);
                }

            } 
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {





            var maBaiBao = txtMaBaiBao.Text.Trim();
            var tenBaiBao = txtTenBaiBao.Text.Trim();
            var moTa = txtMoTa.Text.Trim();
            var nam = txtNam.Text.Trim();
            var quocGia = txtQuocGia.Text.Trim();
            var result = await _context.BaiBaos.Where(x => x.MaBaiBao.Equals(maBaiBao)).FirstOrDefaultAsync();
            if (result != null)
            {
                try
                {
                    result.MoTa = moTa;
                    result.TenBaiBao = tenBaiBao;
                    result.QuocGia = quocGia;
                    result.Nam = int.Parse(nam);
                    _context.Entry(result).State = EntityState.Modified;
                    _context.SaveChanges();
                    fThemTapChiSinhVien_Load(sender, e);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            var maBaiBao = lsBaiBaoSV.SelectedItems[0].SubItems[3].Text;

            var xoaBaiBao = _context.BaiBaos.FirstOrDefault(c => c.MaBaiBao.Equals(maBaiBao));
            if (xoaBaiBao != null)
            {
                _context.BaiBaos.Remove(xoaBaiBao);
                _context.SaveChanges();
                MessageBox.Show("xóa bài báo thành công");
                fThemTapChiSinhVien_Load(sender, e);
            }
            else
            {
                MessageBox.Show("bài báo không tồn tại");
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadingData();
        }

        private async void lsBaiBaoSV_Click(object sender, EventArgs e)
        {
            if (lsBaiBaoSV.SelectedItems.Count == 0)
                return;

            ListViewItem item = lsBaiBaoSV.SelectedItems[0];

            var maGv = item.SubItems[1].Text;
            var sinhviens = await _context.TaiKhoans.Where(x => x.MaTaiKhoan.Equals(maGv)).ToListAsync();
            cbSinhVien.SelectedIndex = sinhviens.FindIndex(x => x.MaTaiKhoan.Equals(maGv));
            txtMaBaiBao.Text = lsBaiBaoSV.SelectedItems[0].SubItems[3].Text;
            txtTenBaiBao.Text = lsBaiBaoSV.SelectedItems[0].SubItems[4].Text;
            txtMoTa.Text = lsBaiBaoSV.SelectedItems[0].SubItems[5].Text;
            txtNam.Text = lsBaiBaoSV.SelectedItems[0].SubItems[6].Text;
            txtQuocGia.Text = lsBaiBaoSV.SelectedItems[0].SubItems[7].Text;
        }

        private void menuQuayLai_Click(object sender, EventArgs e)
        {
            fChucNang  back = new fChucNang();
            this.Hide();
            back.ShowDialog();
        }

        private void menuThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
