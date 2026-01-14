using GarageManagement.DAL.Interfaces;
using GarageManagement.DAL.Repository;
using GarageManagement.Domain;
using System.Threading.Tasks;

namespace GarageManagement.DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GarageContext _context;

        public ICustomerRepository Customers { get; }
        public ICarRepository Cars { get; }
        public IPartRepository Parts { get; }
        public IRepairOrderRespository RepairOrders { get; }

        public UnitOfWork(GarageContext context)
        {
            _context = context;
            Customers = new CustomerRepository(context);
            Cars = new CarRepository(context);
            Parts = new PartRepository(context);
            RepairOrders = new RepairOrderRepository(context);
        }
        public UnitOfWork(GarageContext context, ICustomerRepository customers, ICarRepository cars, IPartRepository parts, IRepairOrderRespository repairOrders)
        {
            _context = context;
            Customers = customers;
            Cars = cars;
            Parts = parts;
            RepairOrders = repairOrders;

        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
