using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.Domain.Entities
{
    public class RepairOrder
    {
        public int RepairOrderId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public int CarId { get; set; }
        public virtual Car Car { get; set; } = null!;
        public virtual ICollection<RepairOrderDetail> Details { get; set; } = new List<RepairOrderDetail>();
    }
}
