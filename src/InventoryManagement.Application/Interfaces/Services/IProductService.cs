using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InventoryManagement.Application.DTOs.Products;

namespace InventoryManagement.Application.Interfaces.Services;

public interface IProductService
{
    Task<List<ProductListDto>> GetAllAsync();

    Task<ProductDetailDto?> GetByIdAsync(Guid id);

    Task CreateAsync(ProductCreateDto dto);

    Task UpdateAsync(ProductUpdateDto dto);

    Task DeleteAsync(Guid id);
}
