using GarageManagement.BLL.DTOs;
using GarageManagement.BLL.Services;
using GarageManagement.DAL;
//using GarageManagement.DAL.Repositories;
using GarageManagement.DAL.Repository;


namespace GarageManagement
{
    public partial class CarForm : Form
    {
        private readonly CarService _carService;
        private readonly CustomerService _customerService;
        private List<CarDto> _currentData = new();

        public CarForm()
        {
            InitializeComponent();

            try
            {
                var context = new GarageContext();
                var customerRepo = new CustomerRepository(context);
                var carRepo = new CarRepository(context);
                var partRepo = new PartRepository(context);
                var repairOrderRepo = new RepairOrderRepository(context);
                var unitOfWork = new UnitOfWork(context, customerRepo, carRepo, partRepo, repairOrderRepo);

                _customerService = new CustomerService(unitOfWork);
                _carService = new CarService(unitOfWork);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void CarForm_Load(object sender, EventArgs e)
        {
            try
            {
                InitCarGrid();
                dgvCars.DataBindingComplete -= dgvCars_DataBindingComplete;
                dgvCars.DataBindingComplete += dgvCars_DataBindingComplete;
                await LoadCustomersAsync();
                await LoadDataAsync();
                ClearInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshGrid()
        {
            try { 
            dgvCars.DataSource = null;
            dgvCars.DataSource = _currentData;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadCustomersAsync()
        {
            try
            {
                var customers = await _customerService.GetAllAsync();
                var list = customers.ToList();
                cmbCustomer.DisplayMember = "FullName";
                cmbCustomer.ValueMember = "CustomerId";
                cmbCustomer.DataSource = list;
                cmbCustomer.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }


        private async Task LoadDataAsync()
        {
            try
            {
                var data = await _carService.GetAllAsync();
                _currentData = data.ToList(); // Dữ liệu đã nằm trên RAM
                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách xe: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInput()
        {
            txtLicensePlate.Text = "";
            txtBrand.Text = "";
            txtModel.Text = "";
            txtYear.Text = "";
            cmbCustomer.SelectedIndex = -1;
            dgvCars.ClearSelection();
        }

        private void dgvCars_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvCars.ClearSelection();
            dgvCars.CurrentCell = null;
        }
        private void InitCarGrid()
        {
            dgvCars.Columns.Clear();
            dgvCars.AutoGenerateColumns = false;

            dgvCars.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "LicensePlate",
                HeaderText = "Biển số",
                Name = "LicensePlate",
                Width = 140
            });

            dgvCars.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Brand",
                HeaderText = "Hãng xe",
                Name = "Brand",
                Width = 140
            });

            dgvCars.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Model",
                HeaderText = "Model",
                Name = "Model",
                Width = 140
            });

            dgvCars.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Year",
                HeaderText = "Năm sản xuất",
                Name = "Year",
                Width = 120
            });

            dgvCars.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CustomerId",
                HeaderText = "Mã khách hàng",
                Name = "CustomerId",
                Width = 140
            });
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbCustomer.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng sở hữu xe.");
                return;
            }
            var dto = new CarDto
            {
                LicensePlate = txtLicensePlate.Text,
                Brand = txtBrand.Text,
                Model = txtModel.Text,
                Year = int.TryParse(txtYear.Text, out var y) ? y : 0,
                CustomerId = (int)cmbCustomer.SelectedValue
            };

            if (string.IsNullOrWhiteSpace(dto.LicensePlate))
            {
                MessageBox.Show("Biển số không được để trống");
                txtLicensePlate.Focus();
                return;
            }

            try
            {
                await _carService.AddAsync(dto);
                await LoadDataAsync(); 
                ClearInput();
                MessageBox.Show("Thêm xe thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (ex.Message.Contains("Biển số")) txtLicensePlate.Focus();
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvCars.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn xe cần sửa");
                return;
            }

            if (cmbCustomer.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng");
                return;
            }

            var id = (int)dgvCars.CurrentRow.Cells["CarId"].Value;

            var dto = new CarDto
            {
                CarId = id,
                LicensePlate = txtLicensePlate.Text.Trim(),
                Brand = txtBrand.Text.Trim(),
                Model = txtModel.Text.Trim(),
                Year = int.TryParse(txtYear.Text, out var y) ? y : 0,
                CustomerId = (int)cmbCustomer.SelectedValue
            };

            if (string.IsNullOrWhiteSpace(dto.LicensePlate))
            {
                MessageBox.Show("Biển số không được để trống");
                return;
            }

            try
            {
                await _carService.UpdateAsync(dto);

                await LoadDataAsync();
                ClearInput();
                MessageBox.Show("Cập nhật xe thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            try { 
            if (dgvCars.CurrentRow == null) return;

            var id = (int)dgvCars.CurrentRow.Cells["CarId"].Value;

            var ok = MessageBox.Show(
                "Xóa xe này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (ok != DialogResult.Yes) return;

            await _carService.DeleteAsync(id);

            var item = _currentData.FirstOrDefault(x => x.CarId == id);
            if (item != null) _currentData.Remove(item);

            RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa xe: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                await LoadDataAsync();
                ClearInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi làm mới dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }   

        private void dgvCars_SelectionChanged(object sender, EventArgs e)
        {
            try { 
            if (dgvCars.CurrentRow == null) return;

            txtLicensePlate.Text = dgvCars.CurrentRow.Cells["LicensePlate"].Value?.ToString();
            txtBrand.Text = dgvCars.CurrentRow.Cells["Brand"].Value?.ToString();
            txtModel.Text = dgvCars.CurrentRow.Cells["Model"].Value?.ToString();
            txtYear.Text = dgvCars.CurrentRow.Cells["Year"].Value?.ToString();

            var customerIdObj = dgvCars.CurrentRow.Cells["CustomerId"].Value;
            if (customerIdObj != null)
            {
                cmbCustomer.SelectedValue = (int)customerIdObj;
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị thông tin xe: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var keyword = txtSearch.Text.Trim();
                if (string.IsNullOrEmpty(keyword))
                {
                    RefreshGrid();
                    return;
                }

                var filtered = _currentData.Where(x =>
                       (x.LicensePlate ?? "").Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                       (x.Brand ?? "").Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                       (x.Model ?? "").Contains(keyword, StringComparison.OrdinalIgnoreCase)
    );
                RefreshGrid(); // LinQ to Objects (client-side) 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm xe: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
