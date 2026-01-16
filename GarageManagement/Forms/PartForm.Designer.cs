using System.Drawing;
using System.Windows.Forms;

namespace GarageManagement.Forms
{
    partial class PartForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlLeft;
        private Panel pnlRight;

        private Panel pnlHeader;
        private Label lblTitle;

        private Panel pnlSearch;
        private TextBox txtSearch;
        private Label lblSearch;

        private Panel pnlGrid;
        private DataGridView dgvParts;

        private Label lblPartName;
        private Label lblUnitPrice;
        private Label lblStock;

        private TextBox txtPartName;
        private TextBox txtUnitPrice;
        private TextBox txtStock;

        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnReset;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlLeft = new Panel();
            this.pnlRight = new Panel();

            this.pnlHeader = new Panel();
            this.lblTitle = new Label();

            this.pnlSearch = new Panel();
            this.txtSearch = new TextBox();
            this.lblSearch = new Label();

            this.pnlGrid = new Panel();
            this.dgvParts = new DataGridView();

            this.lblPartName = new Label();
            this.lblUnitPrice = new Label();
            this.lblStock = new Label();

            this.txtPartName = new TextBox();
            this.txtUnitPrice = new TextBox();
            this.txtStock = new TextBox();

            this.btnAdd = new Button();
            this.btnUpdate = new Button();
            this.btnDelete = new Button();
            this.btnReset = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvParts)).BeginInit();
            this.SuspendLayout();

            // ===== Form =====
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1250, 720);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Quản lý phụ tùng";
            this.Font = new Font("Segoe UI", 10F);
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.WindowState = FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PartForm_Load);

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
            this.lblTitle.Text = "QUẢN LÝ PHỤ TÙNG";
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
            this.dgvParts.Dock = DockStyle.Fill;
            this.dgvParts.RowHeadersVisible = false;
            this.dgvParts.AllowUserToAddRows = false;
            this.dgvParts.AllowUserToDeleteRows = false;
            this.dgvParts.ReadOnly = true;
            this.dgvParts.MultiSelect = false;
            this.dgvParts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            this.dgvParts.AutoGenerateColumns = true;
            this.dgvParts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvParts.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            this.dgvParts.BackgroundColor = Color.White;
            this.dgvParts.BorderStyle = BorderStyle.None;
            this.dgvParts.GridColor = Color.FromArgb(230, 233, 238);
            this.dgvParts.EnableHeadersVisualStyles = false;
            this.dgvParts.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgvParts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
            this.dgvParts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dgvParts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            this.dgvParts.ColumnHeadersHeight = 40;
            this.dgvParts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(227, 242, 253);
            this.dgvParts.DefaultCellStyle.SelectionForeColor = Color.FromArgb(33, 33, 33);
            this.dgvParts.RowTemplate.Height = 34;
            this.dgvParts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 251, 252);
            this.dgvParts.SelectionChanged += new System.EventHandler(this.dgvParts_SelectionChanged);

            this.pnlGrid.Controls.Add(this.dgvParts);

            // ===== Left Layout =====
            this.pnlLeft.Controls.Add(this.pnlGrid);
            this.pnlLeft.Controls.Add(this.pnlSearch);
            this.pnlLeft.Controls.Add(this.pnlHeader);

            // ===== Right Card =====
            Panel card = new Panel();
            card.Dock = DockStyle.Top;
            card.Height = 360;
            card.BackColor = Color.White;
            card.Padding = new Padding(16);

            Font labelFont = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            Color labelColor = Color.FromArgb(55, 71, 79);

            this.lblPartName.AutoSize = true;
            this.lblPartName.Text = "Tên phụ tùng";
            this.lblPartName.Font = labelFont;
            this.lblPartName.ForeColor = labelColor;
            this.lblPartName.Location = new Point(16, 20);

            this.txtPartName.BorderStyle = BorderStyle.FixedSingle;
            this.txtPartName.Location = new Point(16, 46);
            this.txtPartName.Size = new Size(360, 27);

            this.lblUnitPrice.AutoSize = true;
            this.lblUnitPrice.Text = "Đơn giá";
            this.lblUnitPrice.Font = labelFont;
            this.lblUnitPrice.ForeColor = labelColor;
            this.lblUnitPrice.Location = new Point(16, 90);

            this.txtUnitPrice.BorderStyle = BorderStyle.FixedSingle;
            this.txtUnitPrice.Location = new Point(16, 116);
            this.txtUnitPrice.Size = new Size(360, 27);

            this.lblStock.AutoSize = true;
            this.lblStock.Text = "Tồn kho";
            this.lblStock.Font = labelFont;
            this.lblStock.ForeColor = labelColor;
            this.lblStock.Location = new Point(16, 160);

            this.txtStock.BorderStyle = BorderStyle.FixedSingle;
            this.txtStock.Location = new Point(16, 186);
            this.txtStock.Size = new Size(360, 27);

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

            this.btnAdd.Location = new Point(16, 250);
            this.btnUpdate.Location = new Point(150, 250);
            this.btnDelete.Location = new Point(284, 250);
            this.btnReset.Location = new Point(16, 300);

            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);

            this.btnAdd.MouseEnter += (s, e) => this.btnAdd.BackColor = Color.FromArgb(67, 160, 71);
            this.btnAdd.MouseLeave += (s, e) => this.btnAdd.BackColor = Color.FromArgb(76, 175, 80);

            this.btnUpdate.MouseEnter += (s, e) => this.btnUpdate.BackColor = Color.FromArgb(251, 140, 0);
            this.btnUpdate.MouseLeave += (s, e) => this.btnUpdate.BackColor = Color.FromArgb(255, 152, 0);
            
            this.btnDelete.MouseEnter += (s, e) => this.btnDelete.BackColor = Color.FromArgb(229, 57, 53);
            this.btnDelete.MouseLeave += (s, e) => this.btnDelete.BackColor = Color.FromArgb(244, 67, 54);

            this.btnReset.MouseEnter += (s, e) => this.btnReset.BackColor = Color.FromArgb(30, 136, 229);
            this.btnReset.MouseLeave += (s, e) => this.btnReset.BackColor = Color.FromArgb(33, 150, 243);

            card.Controls.Add(this.lblPartName);
            card.Controls.Add(this.txtPartName);
            card.Controls.Add(this.lblUnitPrice);
            card.Controls.Add(this.txtUnitPrice);
            card.Controls.Add(this.lblStock);
            card.Controls.Add(this.txtStock);
            card.Controls.Add(this.btnAdd);
            card.Controls.Add(this.btnUpdate);
            card.Controls.Add(this.btnDelete);
            card.Controls.Add(this.btnReset);

            this.pnlRight.Controls.Add(card);

            // ===== Add to Form =====
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlRight);

            ((System.ComponentModel.ISupportInitialize)(this.dgvParts)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
