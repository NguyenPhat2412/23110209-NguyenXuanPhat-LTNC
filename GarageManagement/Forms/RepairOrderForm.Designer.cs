using System.Drawing;
using System.Windows.Forms;

namespace GarageManagement
{
    partial class RepairOrderForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlLeft;
        private Panel pnlRight;

        private Panel pnlHeader;
        private Label lblTitle;

        private GroupBox grpInfo;
        private Label lblCar;
        private ComboBox cmbCar;

        private Label lblDescription;
        private TextBox txtDescription;

        private Panel pnlGrid;
        private DataGridView dgvDetails;

        private Panel pnlRightCard;
        private Label lblTotalCaption;
        private Label lblTotalAmount;

        private Button btnSave;

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

            this.grpInfo = new GroupBox();
            this.lblCar = new Label();
            this.cmbCar = new ComboBox();
            this.lblDescription = new Label();
            this.txtDescription = new TextBox();

            this.pnlGrid = new Panel();
            this.dgvDetails = new DataGridView();

            this.pnlRightCard = new Panel();
            this.lblTotalCaption = new Label();
            this.lblTotalAmount = new Label();
            this.btnSave = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).BeginInit();
            this.SuspendLayout();

            // ===== Form =====
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1250, 720);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Lập phiếu sửa chữa";
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.WindowState = FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.RepairOrderForm_Load);

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
            this.lblTitle.Text = "LẬP PHIẾU SỬA CHỮA";
            this.lblTitle.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(33, 150, 243);
            this.lblTitle.Location = new Point(16, 18);

            this.pnlHeader.Controls.Add(this.lblTitle);

            // ===== grpInfo =====
            this.grpInfo.Dock = DockStyle.Top;
            this.grpInfo.Height = 180;
            this.grpInfo.Text = "Thông tin phiếu";
            this.grpInfo.BackColor = Color.White;
            this.grpInfo.ForeColor = Color.FromArgb(55, 71, 79);
            this.grpInfo.Padding = new Padding(14);
            this.grpInfo.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);

            this.lblCar.AutoSize = true;
            this.lblCar.Text = "Xe";
            this.lblCar.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            this.lblCar.ForeColor = Color.FromArgb(55, 71, 79);
            this.lblCar.Location = new Point(18, 35);

            this.cmbCar.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCar.FlatStyle = FlatStyle.Flat;
            this.cmbCar.Location = new Point(18, 58);
            this.cmbCar.Size = new Size(620, 28);

            this.lblDescription.AutoSize = true;
            this.lblDescription.Text = "Mô tả";
            this.lblDescription.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            this.lblDescription.ForeColor = Color.FromArgb(55, 71, 79);
            this.lblDescription.Location = new Point(18, 94);

            this.txtDescription.BorderStyle = BorderStyle.FixedSingle;
            this.txtDescription.Location = new Point(18, 117);
            this.txtDescription.Multiline = true;
            this.txtDescription.ScrollBars = ScrollBars.Vertical;
            this.txtDescription.Size = new Size(720, 45);

            this.grpInfo.Controls.Add(this.lblCar);
            this.grpInfo.Controls.Add(this.cmbCar);
            this.grpInfo.Controls.Add(this.lblDescription);
            this.grpInfo.Controls.Add(this.txtDescription);

            // ===== pnlGrid =====
            this.pnlGrid.Dock = DockStyle.Fill;
            this.pnlGrid.BackColor = Color.White;
            this.pnlGrid.Padding = new Padding(12);

            // ===== dgvDetails =====
            this.dgvDetails.Dock = DockStyle.Fill;

            this.dgvDetails.RowHeadersVisible = false;
            this.dgvDetails.AllowUserToAddRows = true;
            this.dgvDetails.AllowUserToDeleteRows = true;
            this.dgvDetails.MultiSelect = false;
            this.dgvDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            this.dgvDetails.AutoGenerateColumns = false; // bạn init cột trong InitDetailGrid()
            this.dgvDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetails.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            this.dgvDetails.BackgroundColor = Color.White;
            this.dgvDetails.BorderStyle = BorderStyle.None;
            this.dgvDetails.GridColor = Color.FromArgb(230, 233, 238);

            this.dgvDetails.EnableHeadersVisualStyles = false;
            this.dgvDetails.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgvDetails.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
            this.dgvDetails.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dgvDetails.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            this.dgvDetails.ColumnHeadersHeight = 40;

            this.dgvDetails.DefaultCellStyle.BackColor = Color.White;
            this.dgvDetails.DefaultCellStyle.ForeColor = Color.FromArgb(33, 33, 33);
            this.dgvDetails.DefaultCellStyle.SelectionBackColor = Color.FromArgb(227, 242, 253);
            this.dgvDetails.DefaultCellStyle.SelectionForeColor = Color.FromArgb(33, 33, 33);

            this.dgvDetails.RowTemplate.Height = 34;
            this.dgvDetails.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 251, 252);

            this.pnlGrid.Controls.Add(this.dgvDetails);

            // ===== Right Card =====
            this.pnlRightCard.Dock = DockStyle.Top;
            this.pnlRightCard.Height = 210;
            this.pnlRightCard.BackColor = Color.White;
            this.pnlRightCard.Padding = new Padding(16);

            this.lblTotalCaption.AutoSize = true;
            this.lblTotalCaption.Text = "Tổng tiền";
            this.lblTotalCaption.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            this.lblTotalCaption.ForeColor = Color.FromArgb(55, 71, 79);
            this.lblTotalCaption.Location = new Point(16, 18);

            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Text = "0 VNĐ";
            this.lblTotalAmount.Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold);
            this.lblTotalAmount.ForeColor = Color.FromArgb(244, 67, 54);
            this.lblTotalAmount.Location = new Point(16, 50);

            // ===== Save Button =====
            this.btnSave.Text = "Lưu phiếu";
            this.btnSave.Size = new Size(360, 46);
            this.btnSave.Location = new Point(16, 132);
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            this.btnSave.BackColor = Color.FromArgb(76, 175, 80);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Cursor = Cursors.Hand;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnSave.MouseEnter += (s, e) => this.btnSave.BackColor = Color.FromArgb(67, 160, 71);
            this.btnSave.MouseLeave += (s, e) => this.btnSave.BackColor = Color.FromArgb(76, 175, 80);

            this.pnlRightCard.Controls.Add(this.lblTotalCaption);
            this.pnlRightCard.Controls.Add(this.lblTotalAmount);
            this.pnlRightCard.Controls.Add(this.btnSave);

            // ===== Add controls to panels =====
            this.pnlLeft.Controls.Add(this.pnlGrid);
            this.pnlLeft.Controls.Add(this.grpInfo);
            this.pnlLeft.Controls.Add(this.pnlHeader);

            this.pnlRight.Controls.Add(this.pnlRightCard);

            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlRight);

            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
