using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.DAL.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        ICustomerRepository Customers { get; }
        ICarRepository Cars { get; }

        IPartRepository Parts { get; }

        IRepairOrderRespository RepairOrders { get; }

        Task<int> SaveChangesAsync();

    }
}
