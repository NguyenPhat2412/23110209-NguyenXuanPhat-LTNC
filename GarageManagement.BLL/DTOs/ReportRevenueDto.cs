using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.BLL.DTOs
{
    public class ReportRevenueDto
    {
        public DateTime Date { get; set; }
        public decimal TotalRevenue { get; set; }
        public int OrderCount { get; set; }

    }
}
