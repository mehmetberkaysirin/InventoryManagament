using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InventoryManagement.Application.DTOs.Categories;

namespace InventoryManagement.Application.Interfaces.Services;

public interface ICategoryService
{
    Task<List<CategoryListDto>> GetAllAsync();

    Task<CategoryDetailDto?> GetByIdAsync(Guid id);

    Task CreateAsync(CategoryCreateDto dto);

    Task UpdateAsync(CategoryUpdateDto dto);

    Task DeleteAsync(Guid id);
}