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
    public class PartRepository : GenericRepository<Part>, IPartRepository
    {
        public PartRepository(GarageContext context) : base(context)
        {
           
        }

        public async Task<IEnumerable<Part>> SearchByNameAsync(string keyword)
        {
            return await _dbSet
                .Where(p => p.PartName.Contains(keyword))
                .ToListAsync();
        }

    }
}
