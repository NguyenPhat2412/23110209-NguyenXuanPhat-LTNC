using ClosedXML.Excel;
using GarageManagement.BLL.DTOs;
using GarageManagement.DAL.Interfaces;
using GarageManagement.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageManagement.BLL.Services
{
    public class CustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _unitOfWork.Customers.GetAllAsync();
            return customers.Select(x => new CustomerDto
            {
                CustomerId = x.CustomerId,
                FullName = x.FullName,
                Phone = x.Phone,
                Address = x.Address
            });
        }

        public async Task<IEnumerable<CustomerDto>> SearchAsync(string keyword)
        {
            var customers = await _unitOfWork.Customers.SearchByNameAsync(keyword);
            return customers.Select(x => new CustomerDto
            {
                CustomerId = x.CustomerId,
                FullName = x.FullName,
                Phone = x.Phone,
                Address = x.Address
            });
        }

        public async Task AddAsync(CustomerDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FullName))
            {
               throw new Exception("Họ tên không được để trống");
                
                
            }

            if (string.IsNullOrWhiteSpace(dto.Phone))
            {
               throw new Exception( "Số điện thoại không được để trống");
            }

            if (string.IsNullOrWhiteSpace(dto.Address))
            {
                throw new Exception("Địa chỉ không được để trống");
            }

            // Check trùng SĐT (client-side)
            var allCustomers = await _unitOfWork.Customers.GetAllAsync();
            bool isDuplicate = allCustomers.Any(c => c.Phone.Trim() == dto.Phone.Trim());

            if (isDuplicate)
            {
                throw new Exception($"Số điện thoại '{dto.Phone}' đã tồn tại trong hệ thống!");
            }

            var entity = new Customer
            {
                FullName = dto.FullName,
                Phone = dto.Phone,
                Address = dto.Address
            };
            await _unitOfWork.Customers.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(CustomerDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FullName))
                throw new Exception("Họ tên khách hàng không được để trống.");

            if (string.IsNullOrWhiteSpace(dto.Phone))
                throw new Exception("Số điện thoại không được để trống.");

            if (string.IsNullOrWhiteSpace(dto.Address))
                throw new Exception("Địa chỉ không được để trống.");

            var entity = await _unitOfWork.Customers.GetByIdAsync(dto.CustomerId);
            if (entity == null) return;

            var allCustomers = await _unitOfWork.Customers.GetAllAsync();
            bool isDuplicate = allCustomers.Any(c =>
                c.Phone.Trim() == dto.Phone.Trim() &&
                c.CustomerId != dto.CustomerId); 

            if (isDuplicate)
            {
                throw new Exception($"Số điện thoại '{dto.Phone}' đã thuộc về khách hàng khác!");
            }

            entity.FullName = dto.FullName;
            entity.Phone = dto.Phone;
            entity.Address = dto.Address;

            _unitOfWork.Customers.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int customerId)
        {
            var entity = await _unitOfWork.Customers.GetByIdAsync(customerId);
            if (entity == null) return;

            _unitOfWork.Customers.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
