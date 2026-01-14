using GarageManagement.BLL.Services;
using GarageManagement.DAL;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GarageManagement.Forms
{
    public partial class ReportForm : Form
    {
        private readonly ReportService _reportService;

        public ReportForm()
        {
            InitializeComponent();

            var context = new GarageContext();
            _reportService = new ReportService(context);

            dtFrom.Value = DateTime.Today.AddDays(-30);
            dtTo.Value = DateTime.Today;
        }

        private async void ReportForm_Load(object sender, EventArgs e)
        {
            InitRevenueGrid();
            await LoadReportAsync();
        }

        private async void btnRun_Click(object sender, EventArgs e)
        {
            await LoadReportAsync();
        }

        private void InitRevenueGrid()
        {
            dgvRevenue.Columns.Clear();
            dgvRevenue.AutoGenerateColumns = false;

            dgvRevenue.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Date",
                DataPropertyName = "Date",
                HeaderText = "Ngày",
                Width = 120,
                DefaultCellStyle = { Format = "dd/MM/yyyy" }
            });

            dgvRevenue.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalRevenue",
                DataPropertyName = "TotalRevenue",
                HeaderText = "Doanh thu (VNĐ)",
                Width = 160,
                DefaultCellStyle = { Format = "N0" }
            });

            dgvRevenue.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "OrderCount",
                DataPropertyName = "OrderCount",
                HeaderText = "Số lượng đơn hàng",
                Width = 160,
                DefaultCellStyle = { Format = "N0" }
            });
        }

        private async Task LoadReportAsync()
        {
            var from = dtFrom.Value.Date;
            var to = dtTo.Value.Date;

            if (from > to)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc.");
                return;
            }

            // gọi service
            var rows = (await _reportService.GetRevenueByDateRangeAsync(from, to)).ToList();

            dgvRevenue.DataSource = null;
            dgvRevenue.AutoGenerateColumns = true;
            dgvRevenue.DataSource = rows;

            // format cột ngày (nếu có)
            if (dgvRevenue.Columns["Date"] != null)
                dgvRevenue.Columns["Date"].DefaultCellStyle.Format = "dd/MM/yyyy";

            // format cột doanh thu (nếu có)
            if (dgvRevenue.Columns["TotalRevenue"] != null)
                dgvRevenue.Columns["TotalRevenue"].DefaultCellStyle.Format = "N0";

            var total = rows.Sum(x => x.TotalRevenue);
            lblTotal.Text = $"{total:N0} VNĐ";

            if (rows.Count == 0)
            {
                // để bạn biết không có dữ liệu chứ không phải lỗi
                // MessageBox.Show("Không có dữ liệu trong khoảng thời gian đã chọn.");
            }
        }

        private async void btnExportExcel_Click(object sender, EventArgs e)
        {
            var from = dtFrom.Value.Date;
            var to = dtTo.Value.Date;

            if (from > to)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc.");
                return;
            }

            var rows = (await _reportService.GetRevenueByDateRangeAsync(from, to)).ToList();
            if (rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.");
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                FileName = $"DoanhThu_{from:yyyyMMdd}_{to:yyyyMMdd}.xlsx"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                ExcelExporter.ExportRevenueToExcel(rows, from, to, sfd.FileName);
                MessageBox.Show("Xuất Excel thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + (ex.InnerException?.Message ?? ex.Message));
            }
        }
    }
}
