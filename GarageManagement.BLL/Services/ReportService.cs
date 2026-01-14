using GarageManagement.BLL.DTOs;
using GarageManagement.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageManagement.BLL.Services
{
    public class ReportService
    {
        private readonly GarageContext _context;

        public ReportService(GarageContext context)
        {
            _context = context;
        }

        public async Task<List<ReportRevenueDto>> GetRevenueByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            var from = fromDate.Date;
            var toExclusive = toDate.Date.AddDays(1);

            var rows = await _context.RepairOrders
                .AsNoTracking()
                .Where(x => x.CreatedDate >= from && x.CreatedDate < toExclusive)
                .GroupBy(x => x.CreatedDate.Date)
                .Select(g => new ReportRevenueDto
                {
                    Date = g.Key,
                    OrderCount = g.Count(),
                    TotalRevenue = g.Sum(x => x.TotalAmount)
                })
                .OrderBy(x => x.Date)
                .ToListAsync();

            return rows;
        }
    }
}
