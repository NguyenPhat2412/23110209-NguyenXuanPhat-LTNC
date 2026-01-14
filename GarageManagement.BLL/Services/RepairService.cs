using GarageManagement.DAL;
using GarageManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageManagement.BLL.Services
{
    public class RepairService
    {
        private readonly GarageContext _context;

        public RepairService(GarageContext context)
        {
            _context = context;
        }

        public async Task<int> CreateRepairOrderAsync(
            int carId,
            string description,
            IList<(int partId, int quantity, decimal laborCost)> lines)
        {
            // Đánh dấu điểm bắt đầu. mọi thay đổi Db sau dòng này đều là "Tạm thời" cho đến khi commit
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var carExists = await _context.Cars.AnyAsync(x => x.CarId == carId);
                if (!carExists) throw new Exception($"Không tìm thấy xe CarId={carId}");

                var grouped = lines
                    .Where(x => x.quantity > 0)
                    .GroupBy(x => x.partId)
                    .Select(g => new
                    {
                        PartId = g.Key,
                        Quantity = g.Sum(x => x.quantity),
                        LaborCost = g.Sum(x => x.laborCost)
                    })
                    .ToList();

                if (grouped.Count == 0) throw new Exception("Chưa có dòng chi tiết hợp lệ.");

                var partIds = grouped.Select(x => x.PartId).ToList();

                var parts = await _context.Parts
                    .Where(p => partIds.Contains(p.PartId))
                    .ToListAsync();

                if (parts.Count != partIds.Count)
                {
                    var missing = partIds.Except(parts.Select(p => p.PartId)).ToList();
                    throw new Exception("Không tìm thấy phụ tùng: " + string.Join(", ", missing));
                }

                foreach (var g in grouped)
                {
                    var part = parts.First(p => p.PartId == g.PartId);
                    if (part.StockQuantity < g.Quantity)
                        throw new Exception($"Phụ tùng '{part.PartName}' không đủ tồn kho. Tồn: {part.StockQuantity}, cần: {g.Quantity}");
                }

                // Lưu phiếu sửa chữa 
                var order = new RepairOrder
                {
                    CarId = carId,
                    Description = description ?? "",
                    CreatedDate = DateTime.Now,
                    TotalAmount = 0m
                };

                await _context.RepairOrders.AddAsync(order);
                await _context.SaveChangesAsync();

                decimal total = 0m;

                foreach (var g in grouped)
                {

                    // Cập nhật trừ tồn kho phụ tùng 
                    var part = parts.First(p => p.PartId == g.PartId);

                    var lineTotal = (part.UnitPrice * g.Quantity) + g.LaborCost;

                    // Lưu chi tiết phiếu (detail) 
                    var detail = new RepairOrderDetail
                    {
                        RepairOrderId = order.RepairOrderId,
                        PartId = part.PartId,
                        Quantity = g.Quantity,
                        LaborCost = g.LaborCost,
                        LineTotal = lineTotal,
                        Part = null,
                        RepairOrder = null,
                    };

                    await _context.RepairOrderDetails.AddAsync(detail);

                    part.StockQuantity -= g.Quantity;
                    //_context.Parts.Update(part);

                    total += lineTotal;
                }

                order.TotalAmount = total;
                _context.RepairOrders.Update(order);

                await _context.SaveChangesAsync();

                // Đảm bảo: Nếu lưu được là lưu cả Phiếu + Chi tiết + trừ tồn kho
                await transaction.CommitAsync();

                return order.RepairOrderId;
            }
            catch
            {
                // Rollback (hoàn tác toàn bô) 
                // Nếu có bất kỳ lỗi nào 
                // Lệnh này sẽ xóa sạch các thao tác đã làm ở trên.
                // Kết quả: không có phiếu rác, kho không bị trừ sai. 
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
