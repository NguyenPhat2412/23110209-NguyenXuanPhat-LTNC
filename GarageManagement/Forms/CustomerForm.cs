using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GarageManagement.BLL.DTOs;
using GarageManagement.BLL.Services;
using GarageManagement.DAL;
//using GarageManagement.DAL.Repositories;
using GarageManagement.DAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace GarageManagement
{
    public partial class CustomerForm : Form
    {
        private readonly CustomerService _customerService;
        private List<CustomerDto> _currentData = new();

        public CustomerForm()
        {
            try { 
            InitializeComponent();

            var context = new GarageContext();

            var customerRepo = new CustomerRepository(context);
            var carRepo = new CarRepository(context);
            var partRepo = new PartRepository(context);
            var repairOrderRepo = new RepairOrderRepository(context);
            var unitOfWork = new UnitOfWork(context, customerRepo, carRepo, partRepo, repairOrderRepo);

            _customerService = new CustomerService(unitOfWork);
                            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo form khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }   

        private async void CustomerForm_Load(object sender, EventArgs e)
        {
            try { 
            InitCustomerGrid();
            dgvCustomers.DataBindingComplete -= dgvCustomers_DataBindingComplete;
            dgvCustomers.DataBindingComplete += dgvCustomers_DataBindingComplete;

            await LoadDataAsync();
            ClearInput();
            } catch(Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCustomers_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            ClearGridSelection();
        }

        private void ClearInput()
        {
            txtFullName.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
        }

        private void ClearGridSelection()
        {
            dgvCustomers.ClearSelection();
            dgvCustomers.CurrentCell = null;
        }
        private async Task LoadDataAsync()
        {
            var data = await _customerService.GetAllAsync();
            _currentData = data.ToList();
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            dgvCustomers.DataSource = null;
            dgvCustomers.DataSource = _currentData;
        }

        private CustomerDto BuildDtoFromInputs(int? id = null)
        {
            return new CustomerDto
            {
                CustomerId = id ?? 0,
                FullName = txtFullName.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Address = txtAddress.Text.Trim()
            };
        }

        private void InitCustomerGrid()
        {
            dgvCustomers.Columns.Clear();
            dgvCustomers.AutoGenerateColumns = false;

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CustomerId",
                HeaderText = "Mã KH",
                Name = "CustomerId",
                Width = 90
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FullName",
                HeaderText = "Họ và tên",
                Name = "FullName",
                Width = 220
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Phone",
                HeaderText = "Số điện thoại",
                Name = "Phone",
                Width = 140
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Address",
                HeaderText = "Địa chỉ",
                Name = "Address",
                Width = 260
            });
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var dto = BuildDtoFromInputs();
            if (string.IsNullOrWhiteSpace(dto.FullName) ||
                string.IsNullOrWhiteSpace(dto.Phone) ||
                 string.IsNullOrWhiteSpace(dto.Address))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin (Họ tên, SĐT, Địa chỉ).");
                return;
            }

            try { 

            await _customerService.AddAsync(dto);
            _currentData.Add(dto);
            RefreshGrid();

            MessageBox.Show("Thêm khách hàng thành công");
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi nhập liệu: ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa");
                return;
            }
            

            var id = (int)dgvCustomers.CurrentRow.Cells["CustomerId"].Value;
            var dto = BuildDtoFromInputs(id);

            if (string.IsNullOrWhiteSpace(dto.FullName) ||
        string.IsNullOrWhiteSpace(dto.Phone) ||
        string.IsNullOrWhiteSpace(dto.Address))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }
           
           try { 
            await _customerService.UpdateAsync(dto);
            
            var item = _currentData.FirstOrDefault(x => x.CustomerId == id); 
            if(item != null)
            {
                item.Address = dto.Address; 
                item.FullName = dto.FullName;
                item.Phone = dto.Phone;

            }

            RefreshGrid();
            MessageBox.Show("Cập nhật khách hàng thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi nhập liệu: ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow == null) return;

            var id = (int)dgvCustomers.CurrentRow.Cells["CustomerId"].Value;

            var ok = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa khách hàng này không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (ok != DialogResult.Yes) return;

            // thực hiện try-catch xử lý ngoại lệ 
            try { 
            await _customerService.DeleteAsync(id);

            var item = _currentData.FirstOrDefault(x => x.CustomerId == id);
            if (item != null)
                _currentData.Remove(item);

            RefreshGrid();

            MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (DbUpdateException ex) // bắt lỗi
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("FK_Cars_Customers_CustomerId"))
                {
                    MessageBox.Show("Không thể xóa: Khách hàng này đang có xe trong hệ thống!",
                                    "Cảnh báo ràng buộc",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
                else
                {
                    // Các lỗi DB khác
                    MessageBox.Show("Lỗi cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                // Các lỗi khác
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Reset 
        public void btnReset_Click(object sender, EventArgs e)
        {
            ClearInput();
            ClearGridSelection();
        }

        private void dgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow == null) return;
            if (dgvCustomers.SelectedRows.Count == 0) return;

            txtFullName.Text = dgvCustomers.CurrentRow.Cells["FullName"].Value?.ToString();
            txtPhone.Text = dgvCustomers.CurrentRow.Cells["Phone"].Value?.ToString();
            txtAddress.Text = dgvCustomers.CurrentRow.Cells["Address"].Value?.ToString();
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

                var filtered = _currentData
                    .Where(x => (x.FullName ?? "").Contains(keyword, StringComparison.OrdinalIgnoreCase));
                RefreshGrid();
                dgvCustomers.DataSource = null;
                dgvCustomers.DataSource = filtered.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
