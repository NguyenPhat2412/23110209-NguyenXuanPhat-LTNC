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

            var context = new GarageContext();
            _userService = new UserService(context);

            // Nhấn Enter để kích hoạt nút Đăng nhập
            this.AcceptButton = btnLogin;
            this.CancelButton = btnCancel;
            txtUsername.Focus();

        }

        private async void btnLogin_Click(object sender, EventArgs e)
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
