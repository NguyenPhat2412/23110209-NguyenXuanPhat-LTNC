using System.Drawing;
using System.Windows.Forms;

namespace GarageManagement
{
    partial class CarForm
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
        private DataGridView dgvCars;

        private Label lblLicensePlate;
        private Label lblBrand;
        private Label lblModel;
        private Label lblYear;
        private Label lblCustomer;

        private TextBox txtLicensePlate;
        private TextBox txtBrand;
        private TextBox txtModel;
        private TextBox txtYear;
        private ComboBox cmbCustomer;

        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
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
            this.dgvCars = new DataGridView();

            this.lblLicensePlate = new Label();
            this.lblBrand = new Label();
            this.lblModel = new Label();
            this.lblYear = new Label();
            this.lblCustomer = new Label();

            this.txtLicensePlate = new TextBox();
            this.txtBrand = new TextBox();
            this.txtModel = new TextBox();
            this.txtYear = new TextBox();
            this.cmbCustomer = new ComboBox();

            this.btnAdd = new Button();
            this.btnUpdate = new Button();
            this.btnDelete = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvCars)).BeginInit();
            this.SuspendLayout();

            // ===== Form =====
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1250, 720);
            this.Name = "CarForm";
            this.Text = "Quản lý xe";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.WindowState = FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CarForm_Load);

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
            this.lblTitle.Text = "QUẢN LÝ XE";
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
            this.lblSearch.Text = "Tìm biển:";
            this.lblSearch.ForeColor = Color.FromArgb(55, 71, 79);
            this.lblSearch.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            this.lblSearch.Location = new Point(16, 16);

            this.txtSearch.BorderStyle = BorderStyle.FixedSingle;
            this.txtSearch.Location = new Point(86, 12);
            this.txtSearch.Size = new Size(420, 27);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            this.pnlSearch.Controls.Add(this.lblSearch);
            this.pnlSearch.Controls.Add(this.txtSearch);

            // ===== Grid Panel =====
            this.pnlGrid.Dock = DockStyle.Fill;
            this.pnlGrid.BackColor = Color.White;
            this.pnlGrid.Padding = new Padding(12);

            // ===== DataGridView =====
            this.dgvCars.Dock = DockStyle.Fill;
            this.dgvCars.Name = "dgvCars";
            this.dgvCars.RowHeadersVisible = false;
            this.dgvCars.AllowUserToAddRows = false;
            this.dgvCars.AllowUserToDeleteRows = false;
            this.dgvCars.MultiSelect = false;
            this.dgvCars.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvCars.ReadOnly = true;

            this.dgvCars.AutoGenerateColumns = true;
            this.dgvCars.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCars.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            this.dgvCars.BackgroundColor = Color.White;
            this.dgvCars.BorderStyle = BorderStyle.None;
            this.dgvCars.GridColor = Color.FromArgb(230, 233, 238);
            this.dgvCars.EnableHeadersVisualStyles = false;
            this.dgvCars.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgvCars.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
            this.dgvCars.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dgvCars.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            this.dgvCars.ColumnHeadersHeight = 40;
            this.dgvCars.DefaultCellStyle.BackColor = Color.White;
            this.dgvCars.DefaultCellStyle.ForeColor = Color.FromArgb(33, 33, 33);
            this.dgvCars.DefaultCellStyle.SelectionBackColor = Color.FromArgb(227, 242, 253);
            this.dgvCars.DefaultCellStyle.SelectionForeColor = Color.FromArgb(33, 33, 33);
            this.dgvCars.RowTemplate.Height = 34;
            this.dgvCars.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 251, 252);
            this.dgvCars.SelectionChanged += new System.EventHandler(this.dgvCars_SelectionChanged);

            this.pnlGrid.Controls.Add(this.dgvCars);

            // ===== Left layout =====
            this.pnlLeft.Controls.Add(this.pnlGrid);
            this.pnlLeft.Controls.Add(this.pnlSearch);
            this.pnlLeft.Controls.Add(this.pnlHeader);

            // ===== Right Card =====
            Panel card = new Panel();
            card.Dock = DockStyle.Top;
            card.Height = 520;
            card.BackColor = Color.White;
            card.Padding = new Padding(16);

            Font labelFont = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            Color labelColor = Color.FromArgb(55, 71, 79);

            this.lblLicensePlate.AutoSize = true;
            this.lblLicensePlate.Text = "Biển số";
            this.lblLicensePlate.Font = labelFont;
            this.lblLicensePlate.ForeColor = labelColor;
            this.lblLicensePlate.Location = new Point(16, 20);

            this.txtLicensePlate.BorderStyle = BorderStyle.FixedSingle;
            this.txtLicensePlate.Location = new Point(16, 46);
            this.txtLicensePlate.Size = new Size(360, 27);

            this.lblBrand.AutoSize = true;
            this.lblBrand.Text = "Hãng xe";
            this.lblBrand.Font = labelFont;
            this.lblBrand.ForeColor = labelColor;
            this.lblBrand.Location = new Point(16, 90);

            this.txtBrand.BorderStyle = BorderStyle.FixedSingle;
            this.txtBrand.Location = new Point(16, 116);
            this.txtBrand.Size = new Size(360, 27);

            this.lblModel.AutoSize = true;
            this.lblModel.Text = "Model";
            this.lblModel.Font = labelFont;
            this.lblModel.ForeColor = labelColor;
            this.lblModel.Location = new Point(16, 160);

            this.txtModel.BorderStyle = BorderStyle.FixedSingle;
            this.txtModel.Location = new Point(16, 186);
            this.txtModel.Size = new Size(360, 27);

            this.lblYear.AutoSize = true;
            this.lblYear.Text = "Năm";
            this.lblYear.Font = labelFont;
            this.lblYear.ForeColor = labelColor;
            this.lblYear.Location = new Point(16, 230);

            this.txtYear.BorderStyle = BorderStyle.FixedSingle;
            this.txtYear.Location = new Point(16, 256);
            this.txtYear.Size = new Size(360, 27);

            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Text = "Khách hàng";
            this.lblCustomer.Font = labelFont;
            this.lblCustomer.ForeColor = labelColor;
            this.lblCustomer.Location = new Point(16, 300);

            this.cmbCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCustomer.FlatStyle = FlatStyle.Flat;
            this.cmbCustomer.Location = new Point(16, 326);
            this.cmbCustomer.Size = new Size(360, 28);

            Button[] btns = new[] { this.btnAdd, this.btnUpdate, this.btnDelete };
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

            this.btnAdd.BackColor = Color.FromArgb(76, 175, 80);
            this.btnUpdate.BackColor = Color.FromArgb(255, 152, 0);
            this.btnDelete.BackColor = Color.FromArgb(244, 67, 54);

            this.btnAdd.Location = new Point(16, 390);
            this.btnUpdate.Location = new Point(150, 390);
            this.btnDelete.Location = new Point(284, 390);

            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnAdd.MouseEnter += (s, e) => this.btnAdd.BackColor = Color.FromArgb(67, 160, 71);
            this.btnAdd.MouseLeave += (s, e) => this.btnAdd.BackColor = Color.FromArgb(76, 175, 80);

            this.btnUpdate.MouseEnter += (s, e) => this.btnUpdate.BackColor = Color.FromArgb(251, 140, 0);
            this.btnUpdate.MouseLeave += (s, e) => this.btnUpdate.BackColor = Color.FromArgb(255, 152, 0);

            this.btnDelete.MouseEnter += (s, e) => this.btnDelete.BackColor = Color.FromArgb(229, 57, 53);
            this.btnDelete.MouseLeave += (s, e) => this.btnDelete.BackColor = Color.FromArgb(244, 67, 54);

            card.Controls.Add(this.lblLicensePlate);
            card.Controls.Add(this.txtLicensePlate);
            card.Controls.Add(this.lblBrand);
            card.Controls.Add(this.txtBrand);
            card.Controls.Add(this.lblModel);
            card.Controls.Add(this.txtModel);
            card.Controls.Add(this.lblYear);
            card.Controls.Add(this.txtYear);
            card.Controls.Add(this.lblCustomer);
            card.Controls.Add(this.cmbCustomer);
            card.Controls.Add(this.btnAdd);
            card.Controls.Add(this.btnUpdate);
            card.Controls.Add(this.btnDelete);

            this.pnlRight.Controls.Add(card);

            // ===== Add to form =====
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlRight);

            ((System.ComponentModel.ISupportInitialize)(this.dgvCars)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
        