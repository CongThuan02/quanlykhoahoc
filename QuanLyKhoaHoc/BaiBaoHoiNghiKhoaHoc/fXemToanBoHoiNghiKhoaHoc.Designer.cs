namespace QuanLyKhoaHoc.BaiBaoHoiNghiKhoaHoc
{
    partial class fXemToanBoHoiNghiKhoaHoc
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.cbKhoaHoc = new System.Windows.Forms.ComboBox();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MaBaiBao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.btnTimKiem);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(4, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(793, 38);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "<";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(715, 7);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(75, 28);
            this.btnTimKiem.TabIndex = 0;
            this.btnTimKiem.Text = "Tìm Kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listView1);
            this.groupBox1.Controls.Add(this.btnXoa);
            this.groupBox1.Controls.Add(this.btnSua);
            this.groupBox1.Controls.Add(this.btnThem);
            this.groupBox1.Location = new System.Drawing.Point(7, 82);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(787, 356);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh sách bài báo tại hội nghị khoa học";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.MaBaiBao,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 19);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(691, 337);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(184, 7);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(525, 28);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(43, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nhập vào từ khóa:";
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(700, 92);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(75, 28);
            this.btnSua.TabIndex = 0;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(700, 36);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(75, 28);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm ";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(700, 150);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(75, 28);
            this.btnXoa.TabIndex = 0;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            // 
            // cbKhoaHoc
            // 
            this.cbKhoaHoc.FormattingEnabled = true;
            this.cbKhoaHoc.Items.AddRange(new object[] {
            "Toàn bộ",
            "Sinh viên",
            "Cán bộ, Giảng viên"});
            this.cbKhoaHoc.Location = new System.Drawing.Point(694, 50);
            this.cbKhoaHoc.Name = "cbKhoaHoc";
            this.cbKhoaHoc.Size = new System.Drawing.Size(100, 21);
            this.cbKhoaHoc.TabIndex = 2;
            this.cbKhoaHoc.Text = "Toàn bộ";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "STT";
            // 
            // MaBaiBao
            // 
            this.MaBaiBao.Text = "MÃ BÀI BÁO";
            this.MaBaiBao.Width = 78;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Tên bài báo";
            this.columnHeader2.Width = 111;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Chủ Bài báo";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Thời gian đăng bài";
            this.columnHeader4.Width = 116;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Mô tả";
            this.columnHeader5.Width = 201;
            // 
            // fXemToanBoHoiNghiKhoaHoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cbKhoaHoc);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Name = "fXemToanBoHoiNghiKhoaHoc";
            this.Text = "xem toàn bộ các bài báo tại hội nghị khoa học của cán bộ, giảng viên và sinh viên" +
    "";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.ComboBox cbKhoaHoc;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader MaBaiBao;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
    }
}