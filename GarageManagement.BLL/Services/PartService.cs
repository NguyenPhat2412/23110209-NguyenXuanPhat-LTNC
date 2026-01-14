using DocumentFormat.OpenXml.VariantTypes;
using GarageManagement.BLL.DTOs;
using GarageManagement.DAL.Interfaces;
using GarageManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.BLL.Services
{
    public class PartService
    {
        private readonly IUnitOfWork _uow;

        public PartService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<PartDto>> GetAllAsync()
        {
            var parts = await _uow.Parts.GetAllAsync();
            return parts.Select(p => new PartDto
            {
                PartId = p.PartId,
                PartName = p.PartName,
                UnitPrice = p.UnitPrice,
                StockQuantity = p.StockQuantity
            });
        }

        public async Task AddAsync(PartDto dto)
        {
            if (dto.UnitPrice < 0)
            {
                throw new Exception("Đơn giá không được nhỏ hơn 0");
            }

            if(dto.StockQuantity < 0)
            {
                throw new Exception("Số lượng tồn kho không được nhỏ hơn 0");
            } 

            // Validate trung ten 
            var allParts = await _uow.Parts.GetAllAsync();
            if (allParts.Any(p => p.PartName.Equals(dto.PartName, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception($"Đã tồn tại phụ tùng với tên '{dto.PartName}'");
            }

            var entity = new Part
            {
                PartName = dto.PartName,
                UnitPrice = dto.UnitPrice,
                StockQuantity = dto.StockQuantity,
            };

            await _uow.Parts.AddAsync(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task UpdateAsync(PartDto dto)
        {
            if(dto.UnitPrice < 0)
            {
                throw new Exception("Đơn giá không được nhỏ hơn 0");
            }
            if(dto.StockQuantity < 0)
            {
                throw new Exception("Số lượng tồn kho không được nhỏ hơn 0");
            }

            var entity = await _uow.Parts.GetByIdAsync(dto.PartId);
            if (entity == null)
            {
                return;
            }
            
            var allParts = await _uow.Parts.GetAllAsync();
            if(allParts.Any(p => p.PartId != dto.PartId &&
                                  p.PartName.Equals(dto.PartName, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception($"Đã tồn tại phụ tùng với tên '{dto.PartName}'");
            }

            entity.PartName = dto.PartName;
            entity.UnitPrice = dto.UnitPrice;
            entity.StockQuantity = dto.StockQuantity;

            _uow.Parts.Update(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteAsync(int partId)
        {
            var entity = await _uow.Parts.GetByIdAsync(partId);
            if (entity == null)
            {
                return;
            }
            _uow.Parts.Delete(entity);
            await _uow.SaveChangesAsync();
        }

    }
}
