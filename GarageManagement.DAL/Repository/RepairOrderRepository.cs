using GarageManagement.DAL.Interfaces;
using GarageManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.DAL.Repository
{
    public class RepairOrderRepository : GenericRepository<RepairOrder>, IRepairOrderRespository
    {
        public RepairOrderRepository(GarageContext context) : base(context)
        {
        }
        public async Task<IEnumerable<RepairOrder>> GetByDateRangeAsync(DateTime from, DateTime to)
        {
            return await Task.Run(() =>
            {
                return _dbSet.Where(ro => ro.CreatedDate.Date >= from && ro.CreatedDate.Date <= to).ToListAsync();
            });
        }
    }
}
