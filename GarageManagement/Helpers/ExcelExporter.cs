using ClosedXML.Excel;
using GarageManagement.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GarageManagement
{
    public static class ExcelExporter
    {
        public static void ExportRevenueToExcel(List<ReportRevenueDto> rows, DateTime from, DateTime to, string filePath)
        {
            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("DoanhThu");

            ws.Cell(1, 1).Value = "BÁO CÁO DOANH THU";
            ws.Range(1, 1, 1, 3).Merge().Style.Font.Bold = true;
            ws.Cell(2, 1).Value = $"Từ ngày: {from:dd/MM/yyyy}  -  Đến ngày: {to:dd/MM/yyyy}";
            ws.Range(2, 1, 2, 3).Merge();

            ws.Cell(4, 1).Value = "Ngày";
            ws.Cell(4, 2).Value = "Số phiếu";
            ws.Cell(4, 3).Value = "Doanh thu";

            ws.Range(4, 1, 4, 3).Style.Font.Bold = true;
            ws.Range(4, 1, 4, 3).Style.Fill.BackgroundColor = XLColor.FromHtml("#2196F3");
            ws.Range(4, 1, 4, 3).Style.Font.FontColor = XLColor.White;

            int r = 5;
            foreach (var x in rows)
            {
                ws.Cell(r, 1).Value = x.Date;
                ws.Cell(r, 2).Value = x.OrderCount;
                ws.Cell(r, 3).Value = x.TotalRevenue;
                r++;
            }

            var total = rows.Sum(x => x.TotalRevenue);
            ws.Cell(r + 1, 2).Value = "Tổng:";
            ws.Cell(r + 1, 3).Value = total;
            ws.Cell(r + 1, 2).Style.Font.Bold = true;
            ws.Cell(r + 1, 3).Style.Font.Bold = true;

            ws.Column(1).Width = 18;
            ws.Column(2).Width = 12;
            ws.Column(3).Width = 18;
            ws.Column(3).Style.NumberFormat.Format = "#,##0";

            ws.Range(4, 1, r - 1, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Range(4, 1, r - 1, 3).Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            wb.SaveAs(filePath);
        }
    }
}
