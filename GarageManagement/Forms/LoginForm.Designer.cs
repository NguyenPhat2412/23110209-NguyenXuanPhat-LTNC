using System.Drawing;
using System.Windows.Forms;

namespace GarageManagement
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label lblUsername;
        private Label lblPassword;

        private TextBox txtUsername;
        private TextBox txtPassword;

        private Button btnLogin;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblUsername = new Label();
            this.lblPassword = new Label();

            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();

            this.btnLogin = new Button();
            this.btnCancel = new Button();

            this.SuspendLayout();

            // ===== Form =====
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(420, 260);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.Text = "Đăng nhập hệ thống";

            // ===== Title =====
            this.lblTitle.AutoSize = true;
            this.lblTitle.Text = "ĐĂNG NHẬP";
            this.lblTitle.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(33, 150, 243);
            this.lblTitle.Location = new Point(135, 20);

            // ===== Username =====
            this.lblUsername.AutoSize = true;
            this.lblUsername.Text = "Tên tài khoản";
            this.lblUsername.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            this.lblUsername.ForeColor = Color.FromArgb(55, 71, 79);
            this.lblUsername.Location = new Point(50, 80);

            this.txtUsername.Location = new Point(50, 105);
            this.txtUsername.Size = new Size(320, 27);
            this.txtUsername.BorderStyle = BorderStyle.FixedSingle;

            // ===== Password =====
            this.lblPassword.AutoSize = true;
            this.lblPassword.Text = "Mật khẩu";
            this.lblPassword.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            this.lblPassword.ForeColor = Color.FromArgb(55, 71, 79);
            this.lblPassword.Location = new Point(50, 145);

            this.txtPassword.Location = new Point(50, 170);
            this.txtPassword.Size = new Size(320, 27);
            this.txtPassword.BorderStyle = BorderStyle.FixedSingle;
            this.txtPassword.PasswordChar = '●';

            // ===== Login Button =====
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.Size = new Size(150, 38);
            this.btnLogin.Location = new Point(50, 210);
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            this.btnLogin.BackColor = Color.FromArgb(33, 150, 243);
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.Cursor = Cursors.Hand;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            // Hover Login
            this.btnLogin.MouseEnter += (s, e) =>
                this.btnLogin.BackColor = Color.FromArgb(30, 136, 229);
            this.btnLogin.MouseLeave += (s, e) =>
                this.btnLogin.BackColor = Color.FromArgb(33, 150, 243);

            // ===== Cancel Button =====
            this.btnCancel.Text = "Thoát";
            this.btnCancel.Size = new Size(150, 38);
            this.btnCancel.Location = new Point(220, 210);
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            this.btnCancel.BackColor = Color.FromArgb(189, 189, 189);
            this.btnCancel.ForeColor = Color.Black;
            this.btnCancel.Cursor = Cursors.Hand;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // Hover Cancel
            this.btnCancel.MouseEnter += (s, e) =>
                this.btnCancel.BackColor = Color.FromArgb(158, 158, 158);
            this.btnCancel.MouseLeave += (s, e) =>
                this.btnCancel.BackColor = Color.FromArgb(189, 189, 189);

            // ===== Add controls =====
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnCancel);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
