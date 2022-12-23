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

namespace QuanLyKhoaHoc.BaiBaoTrenTapChi.CanBo
{
    public partial class fThemTapChiCanBo : Form
    {
        public fThemTapChiCanBo()
        {
            InitializeComponent();
        }
        private readonly QuanLyKhoaHocEntities _context = new QuanLyKhoaHocEntities();
        private async Task LoadingData()
        {
            var sql = from baibaotapchi in _context.BaiBaos
                      join taiKhoan in _context.TaiKhoans
                      on baibaotapchi.TaiKhoanId equals taiKhoan.Id
                      where baibaotapchi.TenBaiBao.Contains(txtTimKiem.Text) && taiKhoan.LoaiTaiKhoan == 0
                      select new
                      {
                          maCanBo = taiKhoan.MaTaiKhoan,
                          tenGiangVien = taiKhoan.HoTen,
                          maBaiBao = baibaotapchi.MaBaiBao,
                          tenBaiBao = baibaotapchi.TenBaiBao,
                          noiDungBaiBao = baibaotapchi.MoTa,
                          namBaiBao= baibaotapchi.Nam,
                          quocGia = baibaotapchi.QuocGia,
                      };
            var data = await sql.ToListAsync();
            lsBaiBaoCanBo.Items.Clear();
            int index = 1;
            foreach(var item in data)
            {
                var baiBao = new ListViewItem(index.ToString());
                baiBao.SubItems.Add(item.maCanBo);
                baiBao.SubItems.Add(item.tenGiangVien);
                baiBao.SubItems.Add(item.maBaiBao);
                baiBao.SubItems.Add(item.tenBaiBao);
                baiBao.SubItems.Add(item.noiDungBaiBao);
                baiBao.SubItems.Add(item.namBaiBao.ToString());
                baiBao.SubItems.Add(item.quocGia);
                lsBaiBaoCanBo.Items.Add(baiBao);
                index++;

            }
        }


        private async void fThemTapChiCanBo_Load(object sender, EventArgs e)
        {
            await LoadingData();
            lsBaiBaoCanBo.FullRowSelect= true;
            txtTenGiangVien.Enabled= false;
            cbGiangVien.Items.Clear();
            var canBo= await _context.GiangViens.ToListAsync();
            foreach(var cb in canBo)
            {
                var item = new
                {
                    maCanBo = cb.TaiKhoan.MaTaiKhoan,
                    id =cb.Id,
                    hoTen = cb.TaiKhoan.HoTen,
                };
                cbGiangVien.Items.Add(item);
            }
            cbGiangVien.ValueMember = "maCanBo";

        }

        private void cbGiangVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tenCanBo = cbGiangVien.SelectedItem;
            txtTenGiangVien.Text = tenCanBo.GetType()
                .GetProperty("hoTen")
                .GetValue(tenCanBo, null)
                .ToString();
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            string maBaiBao = txtMaBaiBao.Text.Trim();
            string tenBaiBao = txtTenBaiBao.Text.Trim();
            string moTa = txtMoTa.Text.Trim();
            string nam = txtNam.Text.Trim();
            string quocGia = txtQuocGia.Text.Trim();
            var current = cbGiangVien.SelectedItem;
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
                if (maBaiBao == "" || tenBaiBao == "" || moTa == "" || quocGia=="" || nam=="")
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
                        QuocGia= quocGia,
                        
                    };
                    _context.BaiBaos.Add(baibao);
                    await _context.SaveChangesAsync();
                    MessageBox.Show("Thêm bài báo thành công");
                    txtMaBaiBao.Clear();
                    txtMoTa.Clear();
                    txtTenBaiBao.Clear();
                    fThemTapChiCanBo_Load(sender, e);
                }

            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var maBaiBao = lsBaiBaoCanBo.SelectedItems[0].SubItems[3].Text;
          
            var xoaBaiBao = _context.BaiBaos.FirstOrDefault(c => c.MaBaiBao.Equals(maBaiBao));
            if (xoaBaiBao != null)
            {
                _context.BaiBaos.Remove(xoaBaiBao);
                _context.SaveChanges();
                MessageBox.Show("xóa bài báo thành công");
                fThemTapChiCanBo_Load(sender, e);
            }
            else
            {
                MessageBox.Show("bài báo không tồn tại");
            }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            var maBaiBao = txtMaBaiBao.Text.Trim();
            var tenBaiBao = txtTenBaiBao.Text.Trim();
            var moTa = txtMoTa.Text.Trim();
            var nam =txtNam.Text.Trim();
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
                    fThemTapChiCanBo_Load(sender, e);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadingData();
        }

        private async Task lsBaiBaoCanBo_SelectedIndexChangedAsync(object sender, EventArgs e)
        {
            
        }

        private async void lsBaiBaoCanBo_Click(object sender, EventArgs e)
        {
            if (lsBaiBaoCanBo.SelectedItems.Count == 0)
                return;

            ListViewItem item = lsBaiBaoCanBo.SelectedItems[0];

            var maGv = item.SubItems[1].Text;
            var sinhviens = await _context.TaiKhoans.Where(x => x.MaTaiKhoan.Equals(maGv)).ToListAsync();
            cbGiangVien.SelectedIndex = sinhviens.FindIndex(x => x.MaTaiKhoan.Equals(maGv));
            txtMaBaiBao.Text = lsBaiBaoCanBo.SelectedItems[0].SubItems[3].Text;
            txtTenBaiBao.Text = lsBaiBaoCanBo.SelectedItems[0].SubItems[4].Text;
            txtMoTa.Text = lsBaiBaoCanBo.SelectedItems[0].SubItems[5].Text;
            txtNam.Text = lsBaiBaoCanBo.SelectedItems[0].SubItems[6].Text; 
            txtQuocGia.Text = lsBaiBaoCanBo.SelectedItems[0].SubItems[7].Text;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void lsBaiBaoCanBo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtNam_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

       

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuQuayLai_Click(object sender, EventArgs e)
        {
            fChucNang back =new fChucNang();
            this.Hide();
            back.ShowDialog();
        }

        private void menuThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtMoTa_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtTenBaiBao_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtMaBaiBao_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtTenGiangVien_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
