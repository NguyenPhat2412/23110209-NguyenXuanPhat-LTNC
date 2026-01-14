using GarageManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.DAL.Interfaces
{
    public interface IRepairOrderRespository : IGenericRepository<RepairOrder>
    {
        Task<IEnumerable<RepairOrder>> GetByDateRangeAsync(DateTime from, DateTime to);
    }
}
