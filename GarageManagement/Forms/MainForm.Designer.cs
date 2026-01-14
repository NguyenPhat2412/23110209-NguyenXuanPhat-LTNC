using System.Drawing;
using System.Windows.Forms;

namespace GarageManagement
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlSidebar;
        private Panel pnlHeader;
        private Panel pnlContent;

        private Label lblTitle;
        private Label lblSubTitle;

        private Button btnCustomers;
        private Button btnCars;
        private Button btnRepairOrders;
        private Button btnParts;
        private Button btnReports;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlSidebar = new Panel();
            this.pnlHeader = new Panel();
            this.pnlContent = new Panel();

            this.lblTitle = new Label();
            this.lblSubTitle = new Label();

            this.btnCustomers = new Button();
            this.btnCars = new Button();
            this.btnRepairOrders = new Button();
            this.btnParts = new Button();
            this.btnReports = new Button();

            this.SuspendLayout();

            // ===== Form =====
            this.ClientSize = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Quản lý gara ô tô";
            this.Font = new Font("Segoe UI", 10F);
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Maximized;

            // ===== Sidebar =====
            this.pnlSidebar.Dock = DockStyle.Left;
            this.pnlSidebar.Width = 260;
            this.pnlSidebar.BackColor = Color.FromArgb(33, 150, 243);

            // ===== Header =====
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Height = 120;
            this.pnlHeader.BackColor = Color.White;

            // ===== Content =====
            this.pnlContent.Dock = DockStyle.Fill;
            this.pnlContent.BackColor = Color.FromArgb(245, 247, 250);

            // ===== Header title =====
            this.lblTitle.Text = "HỆ THỐNG QUẢN LÝ GARA Ô TÔ";
            this.lblTitle.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(33, 150, 243);
            this.lblTitle.Location = new Point(40, 30);
            this.lblTitle.AutoSize = true;

            this.lblSubTitle.Text = "Chọn chức năng ở menu bên trái";
            this.lblSubTitle.Font = new Font("Segoe UI", 11F);
            this.lblSubTitle.ForeColor = Color.FromArgb(96, 125, 139);
            this.lblSubTitle.Location = new Point(42, 75);
            this.lblSubTitle.AutoSize = true;

            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubTitle);

            // ===== Sidebar buttons style =====
            Button[] menuButtons =
            {
                btnCustomers, btnCars, btnRepairOrders, btnParts, btnReports
            };

            foreach (var btn in menuButtons)
            {
                btn.Dock = DockStyle.Top;
                btn.Height = 70;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Padding = new Padding(20, 0, 0, 0);
                btn.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
                btn.ForeColor = Color.White;
                btn.Cursor = Cursors.Hand;
            }

            // ===== Buttons =====
            this.btnCustomers.Text = "👤  Quản lý khách hàng";
            this.btnCustomers.Click += new System.EventHandler(this.btnCustomers_Click);

            this.btnCars.Text = "🚗  Quản lý xe";
            this.btnCars.Click += new System.EventHandler(this.btnCars_Click);

            this.btnRepairOrders.Text = "🧾  Phiếu sửa chữa";
            this.btnRepairOrders.Click += new System.EventHandler(this.btnRepairOrders_Click);

            this.btnParts.Text = "🛠  Quản lý phụ tùng";
            this.btnParts.Click += new System.EventHandler(this.btnParts_Click);

            this.btnReports.Text = "📊  Báo cáo doanh thu";
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);

            // Hover effect
            foreach (var btn in menuButtons)
            {
                btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(30, 136, 229);
                btn.MouseLeave += (s, e) => btn.BackColor = Color.FromArgb(33, 150, 243);
                btn.BackColor = Color.FromArgb(33, 150, 243);
            }

            // Add buttons to sidebar (ngược thứ tự vì Dock.Top)
            this.pnlSidebar.Controls.Add(this.btnReports);
            this.pnlSidebar.Controls.Add(this.btnParts);
            this.pnlSidebar.Controls.Add(this.btnRepairOrders);
            this.pnlSidebar.Controls.Add(this.btnCars);
            this.pnlSidebar.Controls.Add(this.btnCustomers);

            // ===== Add panels =====
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlSidebar);

            this.ResumeLayout(false);
        }
    }
}
