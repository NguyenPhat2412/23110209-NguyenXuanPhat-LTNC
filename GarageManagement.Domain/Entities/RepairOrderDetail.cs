using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.Domain.Entities
{
    public class RepairOrderDetail
    {
        public int RepairOrderDetailId { get; set; }
        public int RepairOrderId { get; set; }
        public int PartId { get; set; }
        public int Quantity { get; set; }
        public decimal LaborCost { get; set; }
        public decimal LineTotal { get; set; }
        public virtual RepairOrder? RepairOrder { get; set; } = new RepairOrder();
        public virtual Part? Part { get; set; } = new Part();

    }
}
