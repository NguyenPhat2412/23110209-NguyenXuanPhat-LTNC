using GarageManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.DAL.Interfaces
{
    public interface IPartRepository : IGenericRepository<Part>
    {
        Task<IEnumerable<Part>> SearchByNameAsync(string keyword);
    }
}
