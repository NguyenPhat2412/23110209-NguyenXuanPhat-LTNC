using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.Domain.Entities
{
    public class Car
    {
        public int CarId { get; set; } 
        public string LicensePlate { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public int Year { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual ICollection<RepairOrder> RepairOrders { get; set; } = new List<RepairOrder>();
    }
}
