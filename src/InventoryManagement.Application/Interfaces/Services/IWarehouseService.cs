using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InventoryManagement.Application.DTOs.Warehouses;

namespace InventoryManagement.Application.Interfaces.Services;

public interface IWarehouseService
{
    Task<List<WarehouseListDto>> GetAllAsync();

    Task<WarehouseDetailDto?> GetByIdAsync(Guid id);

    Task CreateAsync(WarehouseCreateDto dto);

    Task UpdateAsync(WarehouseUpdateDto dto);

    Task DeleteAsync(Guid id);
}