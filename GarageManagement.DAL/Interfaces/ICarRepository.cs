using GarageManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.DAL.Interfaces
{
    public interface ICarRepository : IGenericRepository<Car>
    {
        Task<IEnumerable<Car>> GetByCustomerAsync(int customerId);
        Task<IEnumerable<Car>> GetAllWithCustomerAsync();

    }
}
