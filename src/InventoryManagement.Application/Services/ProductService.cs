using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using InventoryManagement.Application.DTOs.Products;
using InventoryManagement.Application.Interfaces.Repositories;
using InventoryManagement.Application.Interfaces.Services;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(
        IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<List<ProductListDto>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();

        return _mapper.Map<List<ProductListDto>>(products);
    }

    public async Task<ProductDetailDto?> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
            return null;

        return _mapper.Map<ProductDetailDto>(product);
    }

    public async Task CreateAsync(ProductCreateDto dto)
    {
        var product = _mapper.Map<Product>(dto);

        product.CreatedAt = DateTime.UtcNow;

        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProductUpdateDto dto)
    {
        var product = await _productRepository.GetByIdAsync(dto.Id);

        if (product == null)
            return;

        product.Name = dto.Name;
        product.Barcode = dto.Barcode ?? string.Empty;
        product.Description = dto.Description;
        product.CategoryId = dto.CategoryId;
        product.BrandId = dto.BrandId;
        product.UnitId = dto.UnitId;
        product.UpdatedAt = DateTime.UtcNow;

        _productRepository.Update(product);

        await _productRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
            return;

        _productRepository.Delete(product);

        await _productRepository.SaveChangesAsync();
    }
}