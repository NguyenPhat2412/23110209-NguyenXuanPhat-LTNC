using GarageManagement.DAL.Interfaces;
//using GarageManagement.DAL.Repositories;
using GarageManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.DAL.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(GarageContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Customer>> SearchByNameAsync(string keyword)
        {
            return await _dbSet
                .Where(c => c.FullName.Contains(keyword))
                .ToListAsync();
        }   
    }
}
