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
            public fThemBaiBaoCanBo()
            {
                InitializeComponent();
            }
            private async void loading()
            {
                lsBaiBaoCanBo.Items.Clear();
                var index = 1;
                var _baiBao = await _context.TaiKhoans.ToListAsync();
            foreach ( var item in _baiBao )
            {
                var items = new ListViewItem(index.ToString());
                items.SubItems.Add(item.Id.ToString());
                items.SubItems.Add(item.HoTen);
                var baibao = await _context.HoiNghiKhoaHocs.Where(h => h.NguoiSoHuuId == item.Id).ToListAsync();
               foreach(var i in baibao)
                {
                    items.SubItems.Add(i.TenBaiBao.ToString());
                };
                lsBaiBaoCanBo.Items.Add(items);
                index++;
                
            }
        }
        private async void fThemBaiBaoCanBo_Load(object sender, EventArgs e)
        {
            txtTenGiangVien.Enabled= false;
            cbGiangVien.Items.Clear();
            var canbos = await _context.GiangViens.ToListAsync();
            foreach(var cb in canbos)
            {
                var item = new
                {
                    _MaTaiKhoan = cb.TaiKhoan.MaTaiKhoan,
                    _Id = cb.Id,
                    _HoTen = cb.TaiKhoan.HoTen
                };
                cbGiangVien.Items.Add(item);
            }

            cbGiangVien.ValueMember = "_Id";
            cbGiangVien.DisplayMember = "_MaTaiKhoan";
            loading();

        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            string _maBaiBao = txtMaBaiBao.Text;
            string _tenBaiBao = txtTenBaiBao.Text;
            string _mota = txtMoTa.Text;
            string _maGiangVien = cbGiangVien.SelectedItem.ToString();
            var current = cbGiangVien.SelectedItem;
            var MaTaiKhoan = current.GetType().GetProperty("_MaTaiKhoan").GetValue(current, null).ToString();

            var id = await _context.TaiKhoans.Where(x => x.MaTaiKhoan.Equals(MaTaiKhoan)).Select(x => x.Id).FirstOrDefaultAsync();
            var idBaiBao = await _context.HoiNghiKhoaHocs.Where(x=> x.MaBaiBao.Equals(_maBaiBao)).Select(x=> x.MaBaiBao).FirstOrDefaultAsync();
            if(idBaiBao!= null)
            {
                txtMaBaiBao.Clear();
                txtMoTa.Clear();
                txtTenBaiBao.Clear();
                MessageBox.Show("Mã bài báo đã tồn tại");
            }
            else
            {
                var hoiNghiKhoaHocCanBo = new HoiNghiKhoaHoc
                {
                    MaBaiBao = _maBaiBao,
                    TenBaiBao = _tenBaiBao,
                    NguoiSoHuuId = id,
                    MoTa = _mota

                };
                _context.HoiNghiKhoaHocs.Add(hoiNghiKhoaHocCanBo);
                await _context.SaveChangesAsync();
                MessageBox.Show("Thêm bài báo thành công");
                txtMaBaiBao.Clear();
                txtMoTa.Clear();
                txtTenBaiBao.Clear();

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
            txtTenGiangVien.Text =  current.GetType().GetProperty("_HoTen").GetValue(current, null).ToString();

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
    }
}
