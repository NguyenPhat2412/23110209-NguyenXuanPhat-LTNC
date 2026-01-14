using DocumentFormat.OpenXml.Wordprocessing;
using GarageManagement.BLL.DTOs;
using GarageManagement.BLL.Services;
using GarageManagement.DAL;
using GarageManagement.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GarageManagement.Forms
{
    public partial class PartForm : Form
    {
        private readonly PartService _partService;
        private List<PartDto> _currentData = new List<PartDto>();

        public PartForm()
        {
            InitializeComponent();

            var context = new GarageContext();
            var uow = new UnitOfWork(context);
            _partService = new PartService(uow);
        }

        private async void PartForm_Load(object sender, EventArgs e)
        {
            InitPartGrid();
            dgvParts.DataBindingComplete -= dgvParts_DataBindingComplete;
            dgvParts.DataBindingComplete += dgvParts_DataBindingComplete;

            await LoadDataAsync();
            ClearInputs();
        }

        private async Task LoadDataAsync()
        {

            var data = await _partService.GetAllAsync();
            _currentData = data.ToList();
            RefreshGrid(_currentData);
        }

        private void RefreshGrid(IEnumerable<PartDto> data)
        {
            dgvParts.DataSource = null;
            dgvParts.DataSource = data.ToList();
        }
        
        private PartDto BuildDtoFromInputs(int? id = null)
        {
            return new PartDto
            {
                PartId = id ?? 0,
                PartName = txtPartName.Text.Trim(),
                UnitPrice = decimal.TryParse(txtUnitPrice.Text, out var p) ? p : 0m,
                StockQuantity = int.TryParse(txtStock.Text, out var q) ? q : 0
            };
        }

        private void ClearInputs()
        {
            txtPartName.Clear();
            txtUnitPrice.Clear();
            txtStock.Clear();
        }

        private void dgvParts_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvParts.ClearSelection();
            dgvParts.CurrentCell = null;
        }

        private void InitPartGrid()
        {
            dgvParts.Columns.Clear();
            dgvParts.AutoGenerateColumns = false;

            dgvParts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PartId",
                Name = "PartId",
                HeaderText = "Mã phụ tùng",
                Width = 110
            });

            dgvParts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PartName",
                Name = "PartName",
                HeaderText = "Tên phụ tùng",
                Width = 220
            });

            dgvParts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UnitPrice",
                Name = "UnitPrice",
                HeaderText = "Đơn giá",
                Width = 120,
                DefaultCellStyle = { Format = "N0" }
            });

            dgvParts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StockQuantity",
                Name = "StockQuantity",
                HeaderText = "Tồn kho",
                Width = 90
            });
        }
        private async void btnAdd_Click(object sender, EventArgs e)
        {
           var dto = BuildDtoFromInputs();  

            
            if (string.IsNullOrWhiteSpace(dto.PartName))
            {
                MessageBox.Show("Tên phụ tùng không được để trống");
                return;
            }

            try
            {
                await _partService.AddAsync(dto);
                _currentData.Add(dto);
                RefreshGrid(_currentData);
                ClearInputs();
                MessageBox.Show("Thêm phụ tùng thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi nhập liệu: ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvParts.CurrentRow == null) return;

            var id = (int)dgvParts.CurrentRow.Cells["PartId"].Value;
            var dto = BuildDtoFromInputs(id);

            if (string.IsNullOrWhiteSpace(dto.PartName))
            {
                MessageBox.Show("Tên phụ tùng không được để trống");
                return;
            }

            try
            {
                await _partService.UpdateAsync(dto);

                var item = _currentData.FirstOrDefault(x => x.PartId == id);
                if (item != null)
                {
                    item.PartName = dto.PartName;
                    item.UnitPrice = dto.UnitPrice;
                    item.StockQuantity = dto.StockQuantity;
                }
                RefreshGrid(_currentData);
                ClearInputs();
                MessageBox.Show("Cập nhật phụ tùng thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi nhập liệu: ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvParts.CurrentRow == null) return;

            var id = (int)dgvParts.CurrentRow.Cells["PartId"].Value;

            var ok = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa phụ tùng này không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (ok != DialogResult.Yes) return;

            await _partService.DeleteAsync(id);

            var item = _currentData.FirstOrDefault(x => x.PartId == id);
            if (item != null)
                _currentData.Remove(item);

            RefreshGrid(_currentData);
            ClearInputs();
        }


        private void dgvParts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvParts.CurrentRow == null) return;
            if (dgvParts.SelectedRows.Count == 0) return;

            txtPartName.Text = dgvParts.CurrentRow.Cells["PartName"].Value?.ToString();
            txtUnitPrice.Text = dgvParts.CurrentRow.Cells["UnitPrice"].Value?.ToString();
            txtStock.Text = dgvParts.CurrentRow.Cells["StockQuantity"].Value?.ToString();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                RefreshGrid(_currentData);
                return;
            }

            var filtered = _currentData.Where(x =>
                (x.PartName ?? "" ) .Contains(keyword, StringComparison.OrdinalIgnoreCase));


            RefreshGrid(filtered);
        }
    }
}
