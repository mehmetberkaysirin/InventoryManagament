using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InventoryManagement.Application.DTOs.Stocks;

namespace InventoryManagement.Application.Interfaces.Services;

public interface IStockService
{
    Task<List<StockListDto>> GetAllAsync();

    Task<StockDetailDto?> GetByIdAsync(Guid id);

    Task CreateAsync(StockCreateDto dto);

    Task UpdateAsync(StockUpdateDto dto);

    Task DeleteAsync(Guid id);
}