using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.BLL.DTOs
{
    public class CarDto
    {
        public int CarId { get; set; }
        public string LicensePlate { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public int Year { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
    }
}
