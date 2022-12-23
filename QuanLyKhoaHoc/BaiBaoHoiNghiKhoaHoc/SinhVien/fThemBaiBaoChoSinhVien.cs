using System;
using System.Collections.Generic;
using QuanLyKhoaHoc.DbConnect;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace QuanLyKhoaHoc.BaiBaoHoiNghiKhoaHoc.SinhVien
{
    public partial class fThemBaiBaoChoSinhVien : Form
    {
        private readonly QuanLyKhoaHocEntities _context = new QuanLyKhoaHocEntities();
       

        public fThemBaiBaoChoSinhVien()
        {
            InitializeComponent();
        }
        private async Task LoadingData()
        {
            
            {
                var sql = from hoiNghiKhoaHoc in _context.HoiNghiKhoaHocs
                          join taiKhoan in _context.TaiKhoans
                          on hoiNghiKhoaHoc.NguoiSoHuuId equals taiKhoan.Id
                          where hoiNghiKhoaHoc.TenBaiBao.Contains(txtTimKiem.Text) && taiKhoan.LoaiTaiKhoan ==1
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
                lsSinhVien.Items.Clear();
                int index = 1;
                foreach (var item in data)
                {
                    var baiBao = new ListViewItem(index.ToString());
                    baiBao.SubItems.Add(item.MSV);
                    baiBao.SubItems.Add(item.TenSinhVien);
                    baiBao.SubItems.Add(item.MaBaiBao);
                    baiBao.SubItems.Add(item.TenBaiBao);
                    baiBao.SubItems.Add(item.Nam.ToString());

                    baiBao.SubItems.Add(item.NoiDungBaiBao);

                    lsSinhVien.Items.Add(baiBao);
                    index++;
                }
            }
        }
     

        private async void btnThem_Click(object sender, EventArgs e)
        {
            string _maBaiBao = txtMaBaiBao.Text.Trim();
            string _tenBaiBao = txttenBaiBao.Text.Trim();
            string _mota = txtMoTa.Text.Trim();
            string _nam = txtNam.Text.Trim();
            string _maGiangVien = cbSinhVien.SelectedItem.ToString();
            var current = cbSinhVien.SelectedItem;
            var MaTaiKhoan = current.GetType().GetProperty("maSinhVien").GetValue(current, null).ToString();

            var id = await _context.TaiKhoans.Where(x => x.MaTaiKhoan.Equals(MaTaiKhoan)).Select(x => x.Id).FirstOrDefaultAsync();
            var idBaiBao = await _context.HoiNghiKhoaHocs.Where(x => x.MaBaiBao.Equals(_maBaiBao)).Select(x => x.MaBaiBao).FirstOrDefaultAsync();
            if (idBaiBao != null)
            {
                txtMaBaiBao.Clear();
                txtMoTa.Clear();
                txttenBaiBao.Clear();
                MessageBox.Show("Mã bài báo đã tồn tại");
            }
            else
            {
                if (_maBaiBao == "" || _tenBaiBao == "" || _mota == "" || _nam == "")
                {
                    MessageBox.Show("vui lòng nhập đầy đủ thông tin cho bài báo");
                }
                else
                {
                    if (int.Parse(_nam) < 2022 && int.Parse(_nam) > 1665)
                    {
                        var hoiNghiKhoaHocCanBo = new HoiNghiKhoaHoc
                        {
                            MaBaiBao = _maBaiBao,
                            TenBaiBao = _tenBaiBao,
                            NguoiSoHuuId = id,
                            MoTa = _mota,
                            Nam = int.Parse(_nam),

                        };
                        _context.HoiNghiKhoaHocs.Add(hoiNghiKhoaHocCanBo);
                        await _context.SaveChangesAsync();
                        MessageBox.Show("Thêm bài báo thành công");
                        txtMaBaiBao.Clear();
                        txtMoTa.Clear();
                        txttenBaiBao.Clear();
                        txtNam.Clear();
                        fThemBaiBaoChoSinhVien_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Năm không hợp lệ ");
                    }
                }

            }

        }

        private async void fThemBaiBaoChoSinhVien_Load(object sender, EventArgs e)
        {
            await LoadingData();
            lsSinhVien.FullRowSelect= true;
            txtHoTen.Enabled= false;
            cbSinhVien.Items.Clear();
            var sinhVien = await _context.SinhViens.ToListAsync();
            foreach(var sv in sinhVien)
            {
                var item = new
                {
                    maSinhVien = sv.TaiKhoan.MaTaiKhoan,
                    id = sv.Id,
                    hoTen = sv.TaiKhoan.HoTen,
                };
                cbSinhVien.Items.Add(item);
            }
            cbSinhVien.ValueMember = "maSinhVien";
          
        }

        
        private void cbSinhVien_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            var current = cbSinhVien.SelectedItem;
            txtHoTen.Text = current.GetType().GetProperty("hoTen").GetValue(current, null).ToString();

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var maBaiBao = lsSinhVien.SelectedItems[0].SubItems[3].Text;
            var xoaBaiBao = _context.HoiNghiKhoaHocs.FirstOrDefault(c => c.MaBaiBao.Equals(maBaiBao));
            if (xoaBaiBao != null)
            {
                _context.HoiNghiKhoaHocs.Remove(xoaBaiBao);
                _context.SaveChanges();
                MessageBox.Show("xóa bài báo thành công");
                fThemBaiBaoChoSinhVien_Load(sender, e);
            }
            else
            {
                MessageBox.Show("bài báo không tồn tại");
            }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            var _maBaiBao = txtMaBaiBao.Text;
            var _tenBaiBao = txttenBaiBao.Text;
            var _nam = txtNam.Text; 
            var _moTa = txtMoTa.Text;
           
            var result = await _context.HoiNghiKhoaHocs.Where(x => x.MaBaiBao.Equals(_maBaiBao)).FirstOrDefaultAsync();
            if (result != null)
            {
                try
                {
                    result.MoTa = _moTa;
                    result.TenBaiBao = _tenBaiBao;
                    result.Nam = int.Parse(_nam);
                    _context.Entry(result).State = EntityState.Modified;
                    _context.SaveChanges();
                    MessageBox.Show("sửa bài báo thành công");
                    fThemBaiBaoChoSinhVien_Load(sender, e);

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

        private async void lsSinhVien_SelectedIndex(object sender, EventArgs e)
        {

          
        }

        private async void lsSinhVien_Click(object sender, EventArgs e)
        {
            if (lsSinhVien.SelectedItems.Count == 0)
                return;

            ListViewItem item = lsSinhVien.SelectedItems[0];

            var maSV = item.SubItems[1].Text;
            var sinhviens = await _context.TaiKhoans.Where(x => x.MaTaiKhoan.Equals(maSV)).ToListAsync();
            cbSinhVien.SelectedIndex = sinhviens.FindIndex(x => x.MaTaiKhoan.Equals(maSV));
            txtMaBaiBao.Text = lsSinhVien.SelectedItems[0].SubItems[3].Text;
            txttenBaiBao.Text = lsSinhVien.SelectedItems[0].SubItems[4].Text;
            txtMoTa.Text = lsSinhVien.SelectedItems[0].SubItems[5].Text;
            txtNam.Text = lsSinhVien.SelectedItems[0].SubItems[6].Text;
        }

        private async  void btnTimKiem_Click(object sender, EventArgs e)
        {
            await LoadingData();
        }

        private void lsSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void menuQuayLai_Click(object sender, EventArgs e)
        {
            fChucNang back = new fChucNang();
            this.Hide();
            back.ShowDialog();
        }
    }
}
