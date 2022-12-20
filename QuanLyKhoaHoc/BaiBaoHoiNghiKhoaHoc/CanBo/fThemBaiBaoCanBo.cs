using QuanLyKhoaHoc.DbConnect;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLyKhoaHoc.BaiBaoHoiNghiKhoaHoc.CanBo
{
    public partial class fThemBaiBaoCanBo : Form
    {
        private readonly QuanLyKhoaHocEntities _context = new QuanLyKhoaHocEntities();
        private List<GiangVien> canBos = new List<GiangVien> { };
        public fThemBaiBaoCanBo()
        {
            InitializeComponent();
        }
        private async void loading()
        {
            lsBaiBaoCanBo.FullRowSelect = true;

        }
        private async Task loadingdata()
        {
            var sql = from hoiNghiKhoaHoc in _context.HoiNghiKhoaHocs
                      join taiKhoan in _context.TaiKhoans
                      on hoiNghiKhoaHoc.NguoiSoHuuId equals taiKhoan.Id
                      where hoiNghiKhoaHoc.TenBaiBao.Contains(txtTimKiem.Text) && (taiKhoan.LoaiTaiKhoan == 0)
                      select new
                      {
                          MaGiangVien = taiKhoan.MaTaiKhoan,
                          TenGiangVien = taiKhoan.HoTen,
                          MaBaiBao = hoiNghiKhoaHoc.MaBaiBao,
                          TenBaiBao = hoiNghiKhoaHoc.TenBaiBao,
                          NoiDungBaiBao = hoiNghiKhoaHoc.MoTa,
                          Nam = hoiNghiKhoaHoc.Nam,
                      };
            var data = await sql.ToListAsync();
            lsBaiBaoCanBo.Items.Clear();
            int index = 1;
            foreach (var item in data)
            {
                var baiBao = new ListViewItem(index.ToString());
                baiBao.SubItems.Add(item.MaGiangVien);
                baiBao.SubItems.Add(item.TenGiangVien);
                baiBao.SubItems.Add(item.MaBaiBao);
                baiBao.SubItems.Add(item.TenBaiBao);
                baiBao.SubItems.Add(item.NoiDungBaiBao);
                baiBao.SubItems.Add(item.Nam.ToString());

                lsBaiBaoCanBo.Items.Add(baiBao);
                index++;
            }
        }
        private async void fThemBaiBaoCanBo_Load(object sender, EventArgs e)
        {
            loading();
            await loadingdata();
            txtTenGiangVien.Enabled = false;
            cbGiangVien.Items.Clear();
            canBos = await _context.GiangViens.ToListAsync();
            foreach (var cb in canBos)
            {
                var item = new
                {
                    _MaTaiKhoan = cb.TaiKhoan.MaTaiKhoan,
                    _Id = cb.Id,
                    _HoTen = cb.TaiKhoan.HoTen
                };
                cbGiangVien.Items.Add(item);
            }

            cbGiangVien.ValueMember = "_MaTaiKhoan";
    

        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            string _maBaiBao = txtMaBaiBao.Text.Trim();
            string _tenBaiBao = txtTenBaiBao.Text.Trim();
            string _mota = txtMoTa.Text.Trim();
            string _nam = txtNam.Text.Trim();
            string _maGiangVien = cbGiangVien.SelectedItem.ToString();
            var current = cbGiangVien.SelectedItem;
            var MaTaiKhoan = current.GetType().GetProperty("_MaTaiKhoan").GetValue(current, null).ToString();

            var id = await _context.TaiKhoans.Where(x => x.MaTaiKhoan.Equals(MaTaiKhoan)).Select(x => x.Id).FirstOrDefaultAsync();
            var idBaiBao = await _context.HoiNghiKhoaHocs.Where(x => x.MaBaiBao.Equals(_maBaiBao)).Select(x => x.MaBaiBao).FirstOrDefaultAsync();
            if (idBaiBao != null)
            {
                txtMaBaiBao.Clear();
                txtMoTa.Clear();
                txtTenBaiBao.Clear();
                MessageBox.Show("Mã bài báo đã tồn tại");
            }
            else
            {
               if(_maBaiBao == "" || _tenBaiBao =="" || _mota == "" || _nam =="")
                {
                    MessageBox.Show("vui lòng nhập đầy đủ thông tin cho bài báo");
                }
                else
                {
                   if(int.Parse(_nam)<2222 && int.Parse(_nam) > 1665)
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
                        txtTenBaiBao.Clear();
                        fThemBaiBaoCanBo_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Năm không hợp lệ ");
                    }
                }

            }



        }

        private void txtTenGiangVien_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbGiangVien_Click(object sender, EventArgs e)
        {


        }

        private void cbGiangVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            var current = cbGiangVien.SelectedItem;
            txtTenGiangVien.Text = current.GetType().GetProperty("_HoTen").GetValue(current, null).ToString();

        }

        private void txtMoTa_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void menuQuayLai_Click(object sender, EventArgs e)
        {
            fChucNang chucNang = new fChucNang();
            this.Hide();
            chucNang.ShowDialog();

        }

        private void menuThoat_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có muốn thoát khỏi chương trình không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var maBaiBao = lsBaiBaoCanBo.SelectedItems[0].SubItems[3].Text;
            var xoaBaiBao = _context.HoiNghiKhoaHocs.FirstOrDefault(c => c.MaBaiBao.Equals(maBaiBao));
            if(xoaBaiBao != null)
            {
                _context.HoiNghiKhoaHocs.Remove(xoaBaiBao);
                _context.SaveChanges();
                MessageBox.Show("xóa bài báo thành công");
                fThemBaiBaoCanBo_Load(sender, e);
            }
            else
            {
                MessageBox.Show("bài báo không tồn tại");
            }

        }

        private void lsBaiBaoCanBo_SelectedIndexChanged(object sender, EventArgs e)
        {
         
           
           
        }

        private async void lsBaiBaoCanBo_Click(object sender, EventArgs e)
        {
            

            if (lsBaiBaoCanBo.SelectedItems.Count == 0)
                return;

            ListViewItem item = lsBaiBaoCanBo.SelectedItems[0];
           
            var maGv = item.SubItems[1].Text;
            var a= cbGiangVien.SelectedIndex = canBos.FindIndex(x => x.TaiKhoan.MaTaiKhoan.Equals(maGv));
            txtMaBaiBao.Text = lsBaiBaoCanBo.SelectedItems[0].SubItems[3].Text;
            txtTenBaiBao.Text = lsBaiBaoCanBo.SelectedItems[0].SubItems[4].Text;
            txtMoTa.Text = lsBaiBaoCanBo.SelectedItems[0].SubItems[5].Text;
           
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            var _maBaiBao = txtMaBaiBao.Text;
            var _tenBaiBao = txtTenBaiBao.Text;
            var _moTa = txtMoTa.Text;
            //MessageBox.Show(_moTa);
            var result = await _context.HoiNghiKhoaHocs.Where(x => x.MaBaiBao.Equals(_maBaiBao)).FirstOrDefaultAsync();
            if (result != null)
            {
                try
                {
                    result.MoTa = _moTa;
                    result.TenBaiBao = _tenBaiBao;
                  
                    _context.Entry(result).State = EntityState.Modified;
                    _context.SaveChanges();
                    fThemBaiBaoCanBo_Load(sender, e);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


        }

        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
          await loadingdata();
           
        }
    }
}
