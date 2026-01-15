using System.Drawing;
using System.Windows.Forms;

namespace GarageManagement
{
    partial class CustomerForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlLeft;
        private Panel pnlRight;

        private Panel pnlHeader;
        private Label lblTitle;

        private Panel pnlSearch;
        private Label lblSearch;
        private TextBox txtSearch;

        private Panel pnlGrid;
        private DataGridView dgvCustomers;

        private Label lblFullName;
        private Label lblPhone;
        private Label lblAddress;

        private TextBox txtFullName;
        private TextBox txtPhone;
        private TextBox txtAddress;

        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnReset;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.pnlLeft = new Panel();
            this.pnlRight = new Panel();

            this.pnlHeader = new Panel();
            this.lblTitle = new Label();

            this.pnlSearch = new Panel();
            this.lblSearch = new Label();
            this.txtSearch = new TextBox();

            this.pnlGrid = new Panel();
            this.dgvCustomers = new DataGridView();

            this.lblFullName = new Label();
            this.lblPhone = new Label();
            this.lblAddress = new Label();

            this.txtFullName = new TextBox();
            this.txtPhone = new TextBox();
            this.txtAddress = new TextBox();

            this.btnAdd = new Button();
            this.btnUpdate = new Button();
            this.btnDelete = new Button();
            this.btnReset = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).BeginInit();
            this.SuspendLayout();

            // ===== Form =====
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1250, 720);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Quản lý khách hàng";
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.WindowState = FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CustomerForm_Load);

            // ===== pnlRight =====
            this.pnlRight.Dock = DockStyle.Right;
            this.pnlRight.Width = 420;
            this.pnlRight.Padding = new Padding(16);
            this.pnlRight.BackColor = Color.FromArgb(245, 247, 250);

            // ===== pnlLeft =====
            this.pnlLeft.Dock = DockStyle.Fill;
            this.pnlLeft.Padding = new Padding(16);
            this.pnlLeft.BackColor = Color.FromArgb(245, 247, 250);

            // ===== Header =====
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Height = 72;
            this.pnlHeader.BackColor = Color.White;
            this.pnlHeader.Padding = new Padding(16, 14, 16, 14);

            this.lblTitle.AutoSize = true;
            this.lblTitle.Text = "QUẢN LÝ KHÁCH HÀNG";
            this.lblTitle.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(33, 150, 243);
            this.lblTitle.Location = new Point(16, 18);

            this.pnlHeader.Controls.Add(this.lblTitle);

            // ===== Search =====
            this.pnlSearch.Dock = DockStyle.Top;
            this.pnlSearch.Height = 56;
            this.pnlSearch.BackColor = Color.White;
            this.pnlSearch.Padding = new Padding(16, 12, 16, 12);

            this.lblSearch.AutoSize = true;
            this.lblSearch.Text = "Tìm tên:";
            this.lblSearch.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            this.lblSearch.ForeColor = Color.FromArgb(55, 71, 79);
            this.lblSearch.Location = new Point(16, 16);

            this.txtSearch.BorderStyle = BorderStyle.FixedSingle;
            this.txtSearch.Location = new Point(80, 12);
            this.txtSearch.Size = new Size(420, 27);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            this.pnlSearch.Controls.Add(this.lblSearch);
            this.pnlSearch.Controls.Add(this.txtSearch);

            // ===== Grid Panel =====
            this.pnlGrid.Dock = DockStyle.Fill;
            this.pnlGrid.BackColor = Color.White;
            this.pnlGrid.Padding = new Padding(12);

            // ===== DataGridView =====
            this.dgvCustomers.Dock = DockStyle.Fill;
            this.dgvCustomers.Name = "dgvCustomers";
            this.dgvCustomers.RowHeadersVisible = false;
            this.dgvCustomers.AllowUserToAddRows = false;
            this.dgvCustomers.AllowUserToDeleteRows = false;
            this.dgvCustomers.ReadOnly = true;
            this.dgvCustomers.MultiSelect = false;
            this.dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            this.dgvCustomers.AutoGenerateColumns = true;
            this.dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCustomers.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            this.dgvCustomers.BackgroundColor = Color.White;
            this.dgvCustomers.BorderStyle = BorderStyle.None;
            this.dgvCustomers.GridColor = Color.FromArgb(230, 233, 238);

            this.dgvCustomers.EnableHeadersVisualStyles = false;
            this.dgvCustomers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgvCustomers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
            this.dgvCustomers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dgvCustomers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            this.dgvCustomers.ColumnHeadersHeight = 40;

            this.dgvCustomers.DefaultCellStyle.BackColor = Color.White;
            this.dgvCustomers.DefaultCellStyle.ForeColor = Color.FromArgb(33, 33, 33);
            this.dgvCustomers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(227, 242, 253);
            this.dgvCustomers.DefaultCellStyle.SelectionForeColor = Color.FromArgb(33, 33, 33);

            this.dgvCustomers.RowTemplate.Height = 34;
            this.dgvCustomers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 251, 252);

            this.dgvCustomers.SelectionChanged += new System.EventHandler(this.dgvCustomers_SelectionChanged);

            this.pnlGrid.Controls.Add(this.dgvCustomers);

            // ===== Left layout =====
            this.pnlLeft.Controls.Add(this.pnlGrid);
            this.pnlLeft.Controls.Add(this.pnlSearch);
            this.pnlLeft.Controls.Add(this.pnlHeader);

            // ===== Right Card =====
            Panel card = new Panel();
            card.Dock = DockStyle.Top;
            card.Height = 480;
            card.BackColor = Color.White;
            card.Padding = new Padding(16);

            Font labelFont = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            Color labelColor = Color.FromArgb(55, 71, 79);

            // lblFullName
            this.lblFullName.AutoSize = true;
            this.lblFullName.Text = "Họ và tên";
            this.lblFullName.Font = labelFont;
            this.lblFullName.ForeColor = labelColor;
            this.lblFullName.Location = new Point(16, 20);

            // txtFullName
            this.txtFullName.BorderStyle = BorderStyle.FixedSingle;
            this.txtFullName.Location = new Point(16, 46);
            this.txtFullName.Size = new Size(360, 27);

            // lblPhone
            this.lblPhone.AutoSize = true;
            this.lblPhone.Text = "Số điện thoại";
            this.lblPhone.Font = labelFont;
            this.lblPhone.ForeColor = labelColor;
            this.lblPhone.Location = new Point(16, 90);

            // txtPhone
            this.txtPhone.BorderStyle = BorderStyle.FixedSingle;
            this.txtPhone.Location = new Point(16, 116);
            this.txtPhone.Size = new Size(360, 27);

            // lblAddress
            this.lblAddress.AutoSize = true;
            this.lblAddress.Text = "Địa chỉ";
            this.lblAddress.Font = labelFont;
            this.lblAddress.ForeColor = labelColor;
            this.lblAddress.Location = new Point(16, 160);

            // txtAddress
            this.txtAddress.BorderStyle = BorderStyle.FixedSingle;
            this.txtAddress.Location = new Point(16, 186);
            this.txtAddress.Size = new Size(360, 90);
            this.txtAddress.Multiline = true;
            this.txtAddress.ScrollBars = ScrollBars.Vertical;

            // Buttons style
            Button[] btns = new[] { this.btnAdd, this.btnUpdate, this.btnDelete, this.btnReset };
            foreach (var b in btns)
            {
                b.FlatStyle = FlatStyle.Flat;
                b.FlatAppearance.BorderSize = 0;
                b.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
                b.ForeColor = Color.White;
                b.Cursor = Cursors.Hand;
                b.Size = new Size(110, 40);
            }

            this.btnAdd.Text = "Thêm";
            this.btnUpdate.Text = "Sửa";
            this.btnDelete.Text = "Xóa";
            this.btnReset.Text = "Làm mới";

            this.btnAdd.BackColor = Color.FromArgb(76, 175, 80);
            this.btnUpdate.BackColor = Color.FromArgb(255, 152, 0);
            this.btnDelete.BackColor = Color.FromArgb(244, 67, 54);
            this.btnReset.BackColor = Color.FromArgb(33, 150, 243);

            this.btnAdd.Location = new Point(16, 310);
            this.btnUpdate.Location = new Point(150, 310);
            this.btnDelete.Location = new Point(284, 310);
            this.btnReset.Location = new Point(16, 370);

            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);

            this.btnAdd.MouseEnter += (s, e) => this.btnAdd.BackColor = Color.FromArgb(67, 160, 71);
            this.btnAdd.MouseLeave += (s, e) => this.btnAdd.BackColor = Color.FromArgb(76, 175, 80);
            //this.btnAdd.MouseHover += (s, e) => this.btnAdd.BackColor = Color.FromArgb(67, 160, 71);

            this.btnUpdate.MouseEnter += (s, e) => this.btnUpdate.BackColor = Color.FromArgb(251, 140, 0);
            this.btnUpdate.MouseLeave += (s, e) => this.btnUpdate.BackColor = Color.FromArgb(255, 152, 0);
            //this.btnDelete.MouseHover += (s, e) => this.btnUpdate.BackColor = Color.FromArgb(251, 140, 0);

            this.btnDelete.MouseEnter += (s, e) => this.btnDelete.BackColor = Color.FromArgb(229, 57, 53);
            this.btnDelete.MouseLeave += (s, e) => this.btnDelete.BackColor = Color.FromArgb(244, 67, 54);
            //this.btnDelete.MouseHover += (s, e) => this.btnDelete.BackColor = Color.FromArgb(229, 57, 53);

            this.btnReset.MouseEnter += (s, e) => this.btnReset.BackColor = Color.FromArgb(30, 136, 229);
            this.btnReset.MouseLeave += (s, e) => this.btnReset.BackColor = Color.FromArgb(33, 150, 243);
            //this.btnReset.MouseHover += (s, e) => this.btnReset.BackColor = Color.FromArgb(30, 136, 229);

            card.Controls.Add(this.lblFullName);
            card.Controls.Add(this.txtFullName);

            card.Controls.Add(this.lblPhone);
            card.Controls.Add(this.txtPhone);

            card.Controls.Add(this.lblAddress);
            card.Controls.Add(this.txtAddress);

            card.Controls.Add(this.btnAdd);
            card.Controls.Add(this.btnUpdate);
            card.Controls.Add(this.btnDelete);
            card.Controls.Add(this.btnReset);

            this.pnlRight.Controls.Add(card);

            // ===== Add to Form =====
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlRight);

            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
