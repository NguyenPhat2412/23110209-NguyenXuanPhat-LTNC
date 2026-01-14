using GarageManagement.BLL.DTOs;
using GarageManagement.BLL.Services;
using GarageManagement.DAL;
using GarageManagement.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GarageManagement
{
    public partial class RepairOrderForm : Form
    {
        private readonly RepairService _repairService;
        private readonly CarService _carService;
        private readonly PartService _partService;

        private List<PartDto> _parts = new();
        private readonly Dictionary<int, int> _stockByPartId = new();

        public RepairOrderForm()
        {
            InitializeComponent();

            var context = new GarageContext();

            var customerRepo = new CustomerRepository(context);
            var carRepo = new CarRepository(context);
            var partRepo = new PartRepository(context);
            var repairOrderRepo = new RepairOrderRepository(context);

            var unitOfWork = new UnitOfWork(context, customerRepo, carRepo, partRepo, repairOrderRepo);

            _repairService = new RepairService(context);
            _carService = new CarService(unitOfWork);
            _partService = new PartService(unitOfWork);
        }

        private async void RepairOrderForm_Load(object sender, EventArgs e)
        {
            await LoadCarsAsync();
            await LoadPartsAsync();
            InitDetailGrid();
            RecalcTotal();
        }

        private async Task LoadCarsAsync()
        {
            var cars = await _carService.GetAllAsync();
            cmbCar.DisplayMember = "LicensePlate";
            cmbCar.ValueMember = "CarId";
            cmbCar.DataSource = cars.ToList();
        }

        private async Task LoadPartsAsync()
        {
            var parts = await _partService.GetAllAsync();
            _parts = parts.ToList();

            _stockByPartId.Clear();
            foreach (var p in _parts)
                _stockByPartId[p.PartId] = p.StockQuantity;
        }

        private void InitDetailGrid()
        {
            dgvDetails.Columns.Clear();
            dgvDetails.AutoGenerateColumns = false;

            // IMPORTANT: ComboBox column hay lỗi "value is not valid"
            dgvDetails.DataError -= dgvDetails_DataError;
            dgvDetails.DataError += dgvDetails_DataError;

            var colPart = new DataGridViewComboBoxColumn
            {
                HeaderText = "Phụ tùng",
                Name = "PartId",
                DataSource = _parts,
                DisplayMember = "PartName",
                ValueMember = "PartId",
                DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox,
                FlatStyle = FlatStyle.Flat,
                Width = 260
            };

            var colUnitPrice = new DataGridViewTextBoxColumn
            {
                HeaderText = "Đơn giá",
                Name = "UnitPrice",
                ReadOnly = true,
                Width = 120
            };

            var colQuantity = new DataGridViewTextBoxColumn
            {
                HeaderText = "Số lượng",
                Name = "Quantity",
                Width = 90
            };

            var colLaborCost = new DataGridViewTextBoxColumn
            {
                HeaderText = "Tiền công",
                Name = "LaborCost",
                Width = 120
            };

            var colLineTotal = new DataGridViewTextBoxColumn
            {
                HeaderText = "Thành tiền",
                Name = "LineTotal",
                ReadOnly = true,
                Width = 140
            };

            dgvDetails.Columns.Add(colPart);
            dgvDetails.Columns.Add(colUnitPrice);
            dgvDetails.Columns.Add(colQuantity);
            dgvDetails.Columns.Add(colLaborCost);
            dgvDetails.Columns.Add(colLineTotal);

            // Event wiring (remove trước để không bị add nhiều lần)
            dgvDetails.CurrentCellDirtyStateChanged -= dgvDetails_CurrentCellDirtyStateChanged;
            dgvDetails.CurrentCellDirtyStateChanged += dgvDetails_CurrentCellDirtyStateChanged;

            dgvDetails.CellValueChanged -= dgvDetails_CellValueChanged;
            dgvDetails.CellValueChanged += dgvDetails_CellValueChanged;

            dgvDetails.RowsAdded -= dgvDetails_RowsAdded;
            dgvDetails.RowsAdded += dgvDetails_RowsAdded;

            dgvDetails.EditingControlShowing -= dgvDetails_EditingControlShowing;
            dgvDetails.EditingControlShowing += dgvDetails_EditingControlShowing;

            dgvDetails.CellEndEdit -= dgvDetails_CellEndEdit;
            dgvDetails.CellEndEdit += dgvDetails_CellEndEdit;

            dgvDetails.RowsRemoved -= dgvDetails_RowsRemoved;
            dgvDetails.RowsRemoved += dgvDetails_RowsRemoved;

            dgvDetails.UserDeletedRow -= dgvDetails_UserDeletedRow;
            dgvDetails.UserDeletedRow += dgvDetails_UserDeletedRow;
        }

        private void dgvDetails_DataError(object? sender, DataGridViewDataErrorEventArgs e)
        {
            // chặn crash combo box, nhất là lúc datasource chưa khớp
            e.ThrowException = false;
        }

        private void dgvDetails_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvDetails.IsCurrentCellDirty)
                dgvDetails.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvDetails_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < e.RowCount; i++)
            {
                var row = dgvDetails.Rows[e.RowIndex + i];
                if (row.IsNewRow) continue;

                row.Cells["Quantity"].Value ??= 1;
                row.Cells["LaborCost"].Value ??= 0m;

                RecalcRow(row);
            }
            RecalcTotal();
        }

        private void dgvDetails_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e) => RecalcTotal();
        private void dgvDetails_UserDeletedRow(object sender, DataGridViewRowEventArgs e) => RecalcTotal();
        private void dgvDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e) => RecalcTotal();

        private void dgvDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvDetails.Rows[e.RowIndex];
            if (row.IsNewRow) return;

            var colName = dgvDetails.Columns[e.ColumnIndex].Name;

            if (colName == "PartId")
            {
                ApplyPartToRow(row);

                // Validate part trùng
                if (!ValidateNoDuplicateParts(row, out var dupMsg))
                {
                    MessageBox.Show(dupMsg);
                    row.Cells["PartId"].Value = null;
                    row.Cells["UnitPrice"].Value = 0m;
                    row.Cells["LineTotal"].Value = 0m;
                    RecalcTotal();
                    return;
                }

                RecalcRow(row);

                // Validate tồn kho
                if (!ValidateRowStock(row, out var stockMsg))
                {
                    MessageBox.Show(stockMsg);
                    row.Cells["Quantity"].Value = 1;
                    RecalcRow(row);
                }

                RecalcTotal();
                return;
            }

            if (colName == "Quantity" || colName == "LaborCost")
            {
                // Validate số lượng/tiền công trước
                NormalizeRowValues(row);

                RecalcRow(row);

                if (!ValidateRowStock(row, out var stockMsg))
                {
                    MessageBox.Show(stockMsg);
                    row.Cells["Quantity"].Value = 1;
                    RecalcRow(row);
                }

                RecalcTotal();
            }
        }

        private void dgvDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvDetails.CurrentCell == null) return;

            var name = dgvDetails.CurrentCell.OwningColumn.Name;

            if (e.Control is TextBox tb)
            {
                tb.KeyPress -= Quantity_KeyPress;
                tb.KeyPress -= Decimal_KeyPress;

                if (name == "Quantity")
                    tb.KeyPress += Quantity_KeyPress;

                if (name == "LaborCost")
                    tb.KeyPress += Decimal_KeyPress;
            }
        }

        private void Quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (char.IsDigit(e.KeyChar)) return;
            e.Handled = true;
        }

        private void Decimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (char.IsDigit(e.KeyChar)) return;
            if (e.KeyChar == '.') return;
            e.Handled = true;
        }

        private void ApplyPartToRow(DataGridViewRow row)
        {
            var partIdObj = row.Cells["PartId"].Value;
            if (partIdObj == null) return;

            var partId = Convert.ToInt32(partIdObj);
            var part = _parts.FirstOrDefault(x => x.PartId == partId);
            if (part == null) return;

            row.Cells["UnitPrice"].Value = part.UnitPrice;
            row.Cells["Quantity"].Value ??= 1;
            row.Cells["LaborCost"].Value ??= 0m;
        }

        private void NormalizeRowValues(DataGridViewRow row)
        {
            // Quantity: >= 1
            var qty = ParseInt(row.Cells["Quantity"].Value);
            if (qty <= 0) qty = 1;
            row.Cells["Quantity"].Value = qty;

            // LaborCost: >= 0
            var labor = ParseDecimal(row.Cells["LaborCost"].Value);
            if (labor < 0) labor = 0m;
            row.Cells["LaborCost"].Value = labor;
        }

        private bool ValidateRowStock(DataGridViewRow row, out string message)
        {
            message = "";

            var partIdObj = row.Cells["PartId"].Value;
            if (partIdObj == null) return true;

            var partId = Convert.ToInt32(partIdObj);
            var qty = ParseInt(row.Cells["Quantity"].Value);

            if (!_stockByPartId.TryGetValue(partId, out var stock))
                return true;

            if (qty > stock)
            {
                var part = _parts.FirstOrDefault(x => x.PartId == partId);
                var name = part?.PartName ?? partId.ToString();
                message = $"Phụ tùng '{name}' không đủ tồn kho. Tồn: {stock}, bạn nhập: {qty}";
                return false;
            }

            return true;
        }

        private bool ValidateNoDuplicateParts(DataGridViewRow currentRow, out string message)
        {
            message = "";

            var partIdObj = currentRow.Cells["PartId"].Value;
            if (partIdObj == null) return true;

            var partId = Convert.ToInt32(partIdObj);

            int count = 0;
            foreach (DataGridViewRow row in dgvDetails.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["PartId"].Value == null) continue;

                if (Convert.ToInt32(row.Cells["PartId"].Value) == partId)
                    count++;
            }

            if (count > 1)
            {
                var part = _parts.FirstOrDefault(x => x.PartId == partId);
                var name = part?.PartName ?? partId.ToString();
                message = $"Phụ tùng '{name}' đã có trong danh sách. Bạn không nên chọn trùng (hãy tăng số lượng ở dòng cũ).";
                return false;
            }

            return true;
        }

        private void RecalcRow(DataGridViewRow row)
        {
            var unitPrice = ParseDecimal(row.Cells["UnitPrice"].Value);
            var qty = ParseInt(row.Cells["Quantity"].Value);
            var labor = ParseDecimal(row.Cells["LaborCost"].Value);

            if (qty < 0) qty = 0;
            if (labor < 0) labor = 0;

            var lineTotal = unitPrice * qty + labor;
            row.Cells["LineTotal"].Value = lineTotal;
        }

        private void RecalcTotal()
        {
            decimal total = 0m;

            foreach (DataGridViewRow row in dgvDetails.Rows)
            {
                if (row.IsNewRow) continue;
                total += ParseDecimal(row.Cells["LineTotal"].Value);
            }

            lblTotalAmount.Text = $"{total:N0} VNĐ";
        }

        private int ParseInt(object? v)
        {
            if (v == null) return 0;
            return int.TryParse(v.ToString(), out var x) ? x : 0;
        }

        private decimal ParseDecimal(object? v)
        {
            if (v == null) return 0m;
            return decimal.TryParse(v.ToString(), out var x) ? x : 0m;
        }

        private bool ValidateBeforeSave(out string message)
        {
            message = "";

            if (cmbCar.SelectedValue == null)
            {
                message = "Bạn chưa chọn xe.";
                return false;
            }

            // ít nhất 1 dòng hợp lệ
            bool anyValid = false;

            foreach (DataGridViewRow row in dgvDetails.Rows)
            {
                if (row.IsNewRow) continue;

                // bỏ qua row trống
                var partObj = row.Cells["PartId"].Value;
                var qty = ParseInt(row.Cells["Quantity"].Value);
                var labor = ParseDecimal(row.Cells["LaborCost"].Value);

                if (partObj == null && qty == 0 && labor == 0m)
                    continue;

                if (partObj == null)
                {
                    message = "Có dòng chưa chọn phụ tùng.";
                    return false;
                }

                if (qty <= 0)
                {
                    message = "Số lượng phải > 0.";
                    return false;
                }

                if (labor < 0)
                {
                    message = "Tiền công không được âm.";
                    return false;
                }

                if (!ValidateRowStock(row, out var stockMsg))
                {
                    message = stockMsg;
                    return false;
                }

                anyValid = true;
            }

            if (!anyValid)
            {
                message = "Chưa có dòng chi tiết hợp lệ.";
                return false;
            }

            // validate trùng part (toàn grid)
            var set = new HashSet<int>();
            foreach (DataGridViewRow row in dgvDetails.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["PartId"].Value == null) continue;

                var pid = Convert.ToInt32(row.Cells["PartId"].Value);
                if (!set.Add(pid))
                {
                    var part = _parts.FirstOrDefault(x => x.PartId == pid);
                    message = $"Bị trùng phụ tùng: {part?.PartName ?? pid.ToString()}";
                    return false;
                }
            }

            return true;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateBeforeSave(out var msg))
            {
                MessageBox.Show(msg);
                return;
            }

            var carId = (int)cmbCar.SelectedValue!;
            var description = txtDescription.Text.Trim();

            var lines = new List<(int partId, int quantity, decimal laborCost)>();

            foreach (DataGridViewRow row in dgvDetails.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["PartId"].Value == null) continue;

                var partId = Convert.ToInt32(row.Cells["PartId"].Value);
                var qty = ParseInt(row.Cells["Quantity"].Value);
                var labor = ParseDecimal(row.Cells["LaborCost"].Value);

                if (qty <= 0) continue;
                lines.Add((partId, qty, labor));
            }

            try
            {
                var id = await _repairService.CreateRepairOrderAsync(carId, description, lines);
                MessageBox.Show("Tạo phiếu thành công. Mã: " + id);

                // Update tồn kho local (để lần sau nhập tiếp vẫn đúng mà không cần reload)
                foreach (var x in lines)
                {
                    if (_stockByPartId.ContainsKey(x.partId))
                        _stockByPartId[x.partId] -= x.quantity;
                }

                dgvDetails.Rows.Clear();
                txtDescription.Clear();
                RecalcTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + (ex.InnerException?.Message ?? ex.Message));
            }
        }
    }
}
