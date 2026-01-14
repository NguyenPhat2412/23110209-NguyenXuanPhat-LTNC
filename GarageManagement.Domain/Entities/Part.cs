using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.Domain.Entities
{
    public class Part
    {
        public int PartId { get; set; }
        public string PartName { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int StockQuantity { get; set; }
    }
}
