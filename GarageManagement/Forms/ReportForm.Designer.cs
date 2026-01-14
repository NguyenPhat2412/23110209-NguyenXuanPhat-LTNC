using System.Drawing;
using System.Windows.Forms;

namespace GarageManagement.Forms
{
    partial class ReportForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlTop;
        private Panel pnlMain;
        private Panel pnlRight;

        private Label lblTitle;
        private Label lblFrom;
        private Label lblTo;

        private DateTimePicker dtFrom;
        private DateTimePicker dtTo;

        private Button btnRun;
        private Button btnExportExcel;

        private DataGridView dgvRevenue;

        private Label lblTotalCaption;
        private Label lblTotal;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTop = new Panel();
            this.pnlMain = new Panel();
            this.pnlRight = new Panel();

            this.lblTitle = new Label();
            this.lblFrom = new Label();
            this.lblTo = new Label();

            this.dtFrom = new DateTimePicker();
            this.dtTo = new DateTimePicker();

            this.btnRun = new Button();
            this.btnExportExcel = new Button();

            this.dgvRevenue = new DataGridView();

            this.lblTotalCaption = new Label();
            this.lblTotal = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvRevenue)).BeginInit();
            this.SuspendLayout();

            // Form
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1200, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Báo cáo doanh thu";
            this.Font = new Font("Segoe UI", 10F);
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.WindowState = FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ReportForm_Load);

            // pnlTop
            this.pnlTop.Dock = DockStyle.Top;
            this.pnlTop.Height = 96;
            this.pnlTop.Padding = new Padding(14, 14, 14, 14);
            this.pnlTop.BackColor = Color.White;

            // title
            this.lblTitle.AutoSize = true;
            this.lblTitle.Text = "BÁO CÁO DOANH THU";
            this.lblTitle.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(33, 150, 243);
            this.lblTitle.Location = new Point(14, 12);

            var labelFont = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            var labelColor = Color.FromArgb(55, 71, 79);

            // from
            this.lblFrom.AutoSize = true;
            this.lblFrom.Text = "Từ ngày";
            this.lblFrom.Font = labelFont;
            this.lblFrom.ForeColor = labelColor;
            this.lblFrom.Location = new Point(18, 54);

            this.dtFrom.Format = DateTimePickerFormat.Short;
            this.dtFrom.Location = new Point(80, 50);
            this.dtFrom.Size = new Size(140, 27);

            // to
            this.lblTo.AutoSize = true;
            this.lblTo.Text = "Đến ngày";
            this.lblTo.Font = labelFont;
            this.lblTo.ForeColor = labelColor;
            this.lblTo.Location = new Point(240, 54);

            this.dtTo.Format = DateTimePickerFormat.Short;
            this.dtTo.Location = new Point(310, 50);
            this.dtTo.Size = new Size(140, 27);

            // btnRun
            this.btnRun.Text = "Xem báo cáo";
            this.btnRun.Size = new Size(140, 32);
            this.btnRun.Location = new Point(470, 48);
            this.btnRun.FlatStyle = FlatStyle.Flat;
            this.btnRun.FlatAppearance.BorderSize = 0;
            this.btnRun.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            this.btnRun.BackColor = Color.FromArgb(33, 150, 243);
            this.btnRun.ForeColor = Color.White;
            this.btnRun.Cursor = Cursors.Hand;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);

            this.btnRun.MouseEnter += (s, e) => this.btnRun.BackColor = Color.FromArgb(30, 136, 229);
            this.btnRun.MouseLeave += (s, e) => this.btnRun.BackColor = Color.FromArgb(33, 150, 243);

            // btnExportExcel
            this.btnExportExcel.Text = "Xuất Excel";
            this.btnExportExcel.Size = new Size(120, 32);
            this.btnExportExcel.Location = new Point(620, 48);
            this.btnExportExcel.FlatStyle = FlatStyle.Flat;
            this.btnExportExcel.FlatAppearance.BorderSize = 0;
            this.btnExportExcel.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            this.btnExportExcel.BackColor = Color.FromArgb(76, 175, 80);
            this.btnExportExcel.ForeColor = Color.White;
            this.btnExportExcel.Cursor = Cursors.Hand;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);

            this.btnExportExcel.MouseEnter += (s, e) => this.btnExportExcel.BackColor = Color.FromArgb(67, 160, 71);
            this.btnExportExcel.MouseLeave += (s, e) => this.btnExportExcel.BackColor = Color.FromArgb(76, 175, 80);

            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Controls.Add(this.lblFrom);
            this.pnlTop.Controls.Add(this.dtFrom);
            this.pnlTop.Controls.Add(this.lblTo);
            this.pnlTop.Controls.Add(this.dtTo);
            this.pnlTop.Controls.Add(this.btnRun);
            this.pnlTop.Controls.Add(this.btnExportExcel);

            // pnlRight
            this.pnlRight.Dock = DockStyle.Right;
            this.pnlRight.Width = 420;
            this.pnlRight.Padding = new Padding(14, 14, 14, 14);
            this.pnlRight.BackColor = Color.White;

            this.lblTotalCaption.AutoSize = true;
            this.lblTotalCaption.Text = "Tổng doanh thu";
            this.lblTotalCaption.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            this.lblTotalCaption.ForeColor = labelColor;
            this.lblTotalCaption.Location = new Point(16, 30);

            this.lblTotal.AutoSize = true;
            this.lblTotal.Text = "0 VNĐ";
            this.lblTotal.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
            this.lblTotal.ForeColor = Color.FromArgb(244, 67, 54);
            this.lblTotal.Location = new Point(16, 60);

            this.pnlRight.Controls.Add(this.lblTotalCaption);
            this.pnlRight.Controls.Add(this.lblTotal);

            // pnlMain
            this.pnlMain.Dock = DockStyle.Fill;
            this.pnlMain.Padding = new Padding(14, 10, 10, 14);

            // dgvRevenue
            this.dgvRevenue.Dock = DockStyle.Fill;
            this.dgvRevenue.RowHeadersVisible = false;
            this.dgvRevenue.AllowUserToAddRows = false;
            this.dgvRevenue.AllowUserToDeleteRows = false;
            this.dgvRevenue.ReadOnly = true;
            this.dgvRevenue.MultiSelect = false;
            this.dgvRevenue.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            this.dgvRevenue.AutoGenerateColumns = true;
            this.dgvRevenue.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.dgvRevenue.BackgroundColor = Color.White;
            this.dgvRevenue.BorderStyle = BorderStyle.None;
            this.dgvRevenue.GridColor = Color.FromArgb(230, 233, 238);
            this.dgvRevenue.EnableHeadersVisualStyles = false;
            this.dgvRevenue.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgvRevenue.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
            this.dgvRevenue.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dgvRevenue.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            this.dgvRevenue.ColumnHeadersHeight = 40;

            this.dgvRevenue.DefaultCellStyle.SelectionBackColor = Color.FromArgb(227, 242, 253);
            this.dgvRevenue.DefaultCellStyle.SelectionForeColor = Color.FromArgb(33, 33, 33);
            this.dgvRevenue.RowTemplate.Height = 34;
            this.dgvRevenue.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 251, 252);

            this.pnlMain.Controls.Add(this.dgvRevenue);

            // add
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlTop);

            ((System.ComponentModel.ISupportInitialize)(this.dgvRevenue)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
