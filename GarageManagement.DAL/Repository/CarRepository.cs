using GarageManagement.DAL.Interfaces;
using GarageManagement.DAL.Repository;
using GarageManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.DAL.Repository
{
    public class CarRepository : GenericRepository<Car>, ICarRepository
    {
        public CarRepository(GarageContext context) : base(context)
        {
        }
        public override async Task<IEnumerable<Car>> GetAllAsync()
        {
            return await _dbSet.Include(x => x.Customer).ToListAsync();
        }

        public async Task<IEnumerable<Car>> GetAllWithCustomerAsync()
        {
                return await _dbSet.Include(x => x.Customer).ToListAsync();
        }


        public async Task<IEnumerable<Car>> GetByCustomerAsync(int customerId)
        {
            return await _dbSet
                .Where(c => c.CustomerId == customerId)
                .ToListAsync();
        }

    }
}
