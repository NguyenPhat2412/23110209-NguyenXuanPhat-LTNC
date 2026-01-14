using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GarageManagement.BLL.DTOs;
using GarageManagement.DAL.Interfaces;
using GarageManagement.Domain.Entities;

namespace GarageManagement.BLL.Services
{
    public class CarService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CarDto>> GetAllAsync()
        {
            var cars = await _unitOfWork.Cars.GetAllWithCustomerAsync();
            return cars.Select(x => new CarDto
            {
                CarId = x.CarId,
                LicensePlate = x.LicensePlate,
                Model = x.Model,
                Brand = x.Brand,
                Year = x.Year,
                CustomerId = x.CustomerId,
                CustomerName = x.Customer != null ? x.Customer.FullName : string.Empty
            });
        }

        public async Task<IEnumerable<CarDto>> GetByCustomerAsync(int customerId)
        {
            var cars = await _unitOfWork.Cars.GetByCustomerAsync(customerId);
            return cars.Select(x => new CarDto
            {
                CarId = x.CarId,
                LicensePlate = x.LicensePlate,
                Model = x.Model,
                Brand = x.Brand,
                Year = x.Year,
                CustomerId = x.CustomerId,
                CustomerName = x.Customer != null ? x.Customer.FullName : string.Empty
            });
        }

        public async Task AddAsync(CarDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.LicensePlate))
                throw new Exception("Biển số xe không được để trống.");

            if (string.IsNullOrWhiteSpace(dto.Brand))
                throw new Exception("Hãng xe không được để trống.");

            if (string.IsNullOrWhiteSpace(dto.Model))
                throw new Exception("Model xe không được để trống.");

            if (dto.Year <= 1900 || dto.Year > DateTime.Now.Year + 1)
                throw new Exception($"Năm sản xuất không hợp lệ (Phải từ 1900 đến {DateTime.Now.Year + 1}).");

            var allCars = await _unitOfWork.Cars.GetAllAsync();
            bool isDuplicate = allCars.Any(c => c.LicensePlate.Equals(dto.LicensePlate, StringComparison.OrdinalIgnoreCase));

            if (isDuplicate)
            {
                throw new Exception($"Biển số xe '{dto.LicensePlate}' đã tồn tại trong hệ thống!");
            }
            var entity = new Car
            {
                LicensePlate = dto.LicensePlate,
                Model = dto.Model,
                Brand = dto.Brand,
                Year = dto.Year,
                CustomerId = dto.CustomerId
            };
            await _unitOfWork.Cars.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(CarDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.LicensePlate))
                throw new Exception("Biển số xe không được để trống.");

            if (string.IsNullOrWhiteSpace(dto.Brand))
                throw new Exception("Hãng xe không được để trống.");

            if (string.IsNullOrWhiteSpace(dto.Model))
                throw new Exception("Model xe không được để trống.");

            if (dto.Year <= 1900 || dto.Year > DateTime.Now.Year + 1)
                throw new Exception("Năm sản xuất không hợp lệ.");

            var entity = await _unitOfWork.Cars.GetByIdAsync(dto.CarId);
            if (entity == null) return;

            var allCars = await _unitOfWork.Cars.GetAllAsync();
            bool isDuplicate = allCars.Any(c =>
                c.LicensePlate.Equals(dto.LicensePlate, StringComparison.OrdinalIgnoreCase) &&
                c.CarId != dto.CarId);

            if (isDuplicate)
            {
                throw new Exception($"Biển số xe '{dto.LicensePlate}' đã thuộc về một xe khác!");
            }

            entity.LicensePlate = dto.LicensePlate;
            entity.Model = dto.Model;
            entity.Brand = dto.Brand;
            entity.Year = dto.Year;
            entity.CustomerId = dto.CustomerId;

            _unitOfWork.Cars.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int carId)
        {
            var entity = await _unitOfWork.Cars.GetByIdAsync(carId);
            if (entity == null) return;

            _unitOfWork.Cars.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
