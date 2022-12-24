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

namespace QuanLyKhoaHoc.QuanLyNhienCuuKhoaHoc.CanBo
{
    public partial class fQuanLyCanBo : Form
    {
        private readonly QuanLyKhoaHocEntities _context = new QuanLyKhoaHocEntities();
        public fQuanLyCanBo()
        {
            InitializeComponent();
        }
        private async Task loadingdata()
        {
            var sql = from nhienCuuKhoaHocCanBo in _context.NghienCuuKhoaHocCanBoes
                      join giangVien in _context.GiangViens

                      on nhienCuuKhoaHocCanBo.GiangVienDangKyId equals giangVien.Id

                      where nhienCuuKhoaHocCanBo.TenDeTai.Contains(txtTimKiem.Text)
                      select new
                      {
                          maGiangVien = giangVien.TaiKhoan.MaTaiKhoan,
                          tenGiangVien = giangVien.TaiKhoan.HoTen,
                          
                        
                          maDeTai = nhienCuuKhoaHocCanBo.MaDeTai,
                          tenDeTai = nhienCuuKhoaHocCanBo.TenDeTai,
                        
                          moTa = nhienCuuKhoaHocCanBo.MoTa,

                      };
            var data = await sql.ToListAsync();
            lsCanBo.Items.Clear();
            int index = 1;
            foreach (var item in data)
            {
                var deTai = new ListViewItem(index.ToString());
                deTai.SubItems.Add(item.maGiangVien);
                deTai.SubItems.Add(item.tenGiangVien);
                deTai.SubItems.Add(item.maDeTai);
                deTai.SubItems.Add(item.tenDeTai);
                deTai.SubItems.Add(item.moTa);


                lsCanBo.Items.Add(deTai);
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
        
        private async void fQuanLyCanBo_Load(object sender, EventArgs e)
        {
            await loadingdata();

            lsCanBo.FullRowSelect = true;
            txtTenGiangVien.Enabled = false;
          
            await getListCanBo();
           
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            string maDeTai = txtMaDeTai.Text.Trim();
            string tenDeTai = txtTenDeTai.Text.Trim();
            string moTa = txtMoTa.Text.Trim();
 
            string maGiangVien = cbCanBo.SelectedItem.ToString();
          
            var curentCb = cbCanBo.SelectedItem;
            var taiKhoanGiangVien = curentCb.GetType().GetProperty("maGiangVien").GetValue(curentCb, null).ToString();

            var idGiangVienDangKy = await _context.GiangViens.Where(x => x.TaiKhoan.MaTaiKhoan.Equals(taiKhoanGiangVien)).Select(x => x.Id).FirstOrDefaultAsync();


            var nhienCuuKhoaHocCanBo = new NghienCuuKhoaHocCanBo
            {
                MaDeTai = maDeTai,
                TenDeTai = tenDeTai,
               
                GiangVienDangKyId = idGiangVienDangKy,
                MoTa = moTa,
              
            };
            _context.NghienCuuKhoaHocCanBoes.Add(nhienCuuKhoaHocCanBo);

            await _context.SaveChangesAsync();
            txtTenDeTai.Clear();

            txtMaDeTai.Clear();
            txtMoTa.Clear();
        
            txtTenGiangVien.Clear();
            MessageBox.Show("thêm thông tin thành công");
            loadingdata();
        }

        private void cbCanBo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var currentCb = cbCanBo.SelectedItem;
            txtTenGiangVien.Text = currentCb.GetType().GetProperty("HoTen").GetValue(currentCb, null).ToString();
        }

       

        private async void btnSua_Click(object sender, EventArgs e)
        {

            var maDeTai = txtMaDeTai.Text;
            var tenDeTai = txtTenDeTai.Text;
     
            var moTa = txtMoTa.Text;
       
            var result = await _context.NghienCuuKhoaHocCanBoes.Where(x => x.MaDeTai.Equals(maDeTai)).FirstOrDefaultAsync();
            if (result != null)
            {
                try
                {
                    result.MoTa = moTa;
                    result.TenDeTai = tenDeTai;
       
                    result.MaDeTai = maDeTai;
                    _context.Entry(result).State = EntityState.Modified;
                    _context.SaveChanges();
                    MessageBox.Show("sửa bài báo thành công");
                    fQuanLyCanBo_Load(sender, e);

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

        private async void lsCanBo_Click(object sender, EventArgs e)
        {
            if (lsCanBo.SelectedItems.Count == 0)
                return;

            ListViewItem item = lsCanBo.SelectedItems[0];

            var maGV = item.SubItems[1].Text;
            var giangVien = await _context.GiangViens.ToListAsync();
            cbCanBo.SelectedIndex = giangVien.FindIndex(x => x.TaiKhoan.MaTaiKhoan.Equals(maGV));
            var maSv = item.SubItems[5].Text;
            var sinhVien = await _context.SinhViens.ToListAsync();
       
            txtMaDeTai.Text = lsCanBo.SelectedItems[0].SubItems[3].Text;
            txtTenDeTai.Text = lsCanBo.SelectedItems[0].SubItems[4].Text;
            txtMoTa.Text = lsCanBo.SelectedItems[0].SubItems[5].Text;
         
        }

        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            await loadingdata();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var maDeTai = lsCanBo.SelectedItems[0].SubItems[3].Text;
            var xoaNhienCuuKhoaHoc = _context.NghienCuuKhoaHocCanBoes.FirstOrDefault(c => c.MaDeTai.Equals(maDeTai));
            if (xoaNhienCuuKhoaHoc != null)
            {
                _context.NghienCuuKhoaHocCanBoes.Remove(xoaNhienCuuKhoaHoc);
                _context.SaveChanges();
                MessageBox.Show("xóa bài báo thành công");
                fQuanLyCanBo_Load(sender, e);
            }
            else
            {
                MessageBox.Show("bài báo không tồn tại");
            }
        }
    }
}
