using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InventoryManagement.Application.DTOs.StockMovements;

namespace InventoryManagement.Application.Interfaces.Services;

public interface IStockMovementService
{
    Task<List<StockMovementListDto>> GetAllAsync();

    Task CreateAsync(StockMovementCreateDto dto);
}