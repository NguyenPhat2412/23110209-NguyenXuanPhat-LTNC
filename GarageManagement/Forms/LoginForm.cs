using GarageManagement.BLL.Services;
using GarageManagement.DAL;

namespace GarageManagement
{
    public partial class LoginForm : Form
    {
        private readonly UserService _userService;

        public LoginForm()
        {
            InitializeComponent();

            try
            {
                var context = new GarageContext();
                _userService = new UserService(context);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }

            // Nhấn Enter để kích hoạt nút Đăng nhập
            this.AcceptButton = btnLogin;
            this.CancelButton = btnCancel;
            txtUsername.Focus();

        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            // Vô hiệu nút đăng nhập tránh nhấn nhiều lần khi đang xử lý 
            btnLogin.Enabled = false;

            try
            {
                var username = txtUsername.Text.Trim();
                var password = txtPassword.Text.Trim();

                var ok = await _userService.ValidateLoginAsync(username, password);
                if (ok)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hủy đăng nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
