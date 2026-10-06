using InventoryManagement.Application.DTOs.Stocks;
using InventoryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Interfaces.Services;

public interface IStockService
{
    Task<List<StockListDto>> GetAllAsync();

    Task<StockDetailDto?> GetByIdAsync(Guid id);

    Task CreateAsync(StockCreateDto dto);

    Task UpdateAsync(StockUpdateDto dto);

    Task DeleteAsync(Guid id);
    Task<Stock?> GetByProductAndWarehouseAsync(Guid productId, Guid warehouseId);
}