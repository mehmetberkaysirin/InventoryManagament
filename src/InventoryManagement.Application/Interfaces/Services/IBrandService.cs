using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InventoryManagement.Application.DTOs.Brands;

namespace InventoryManagement.Application.Interfaces.Services;

public interface IBrandService
{
    Task<List<BrandListDto>> GetAllAsync();

    Task<BrandDetailDto?> GetByIdAsync(Guid id);

    Task CreateAsync(BrandCreateDto dto);

    Task UpdateAsync(BrandUpdateDto dto);

    Task DeleteAsync(Guid id);
}
