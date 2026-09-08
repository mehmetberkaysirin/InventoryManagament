using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InventoryManagement.Application.DTOs.Units;

namespace InventoryManagement.Application.Interfaces.Services;

public interface IUnitService
{
    Task<List<UnitListDto>> GetAllAsync();

    Task<UnitDetailDto?> GetByIdAsync(Guid id);

    Task CreateAsync(UnitCreateDto dto);

    Task UpdateAsync(UnitUpdateDto dto);

    Task DeleteAsync(Guid id);
}