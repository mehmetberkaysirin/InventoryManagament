using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using InventoryManagement.Application.DTOs.Brands;
using InventoryManagement.Application.Interfaces.Repositories;
using InventoryManagement.Application.Interfaces.Services;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Services;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public BrandService(
        IBrandRepository brandRepository,
        IMapper mapper)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    public async Task<List<BrandListDto>> GetAllAsync()
    {
        var brands = await _brandRepository.GetAllAsync();

        return _mapper.Map<List<BrandListDto>>(brands);
    }

    public async Task<BrandDetailDto?> GetByIdAsync(Guid id)
    {
        var brand = await _brandRepository.GetByIdAsync(id);

        if (brand == null)
            return null;

        return _mapper.Map<BrandDetailDto>(brand);
    }

    public async Task CreateAsync(BrandCreateDto dto)
    {
        var brand = _mapper.Map<Brand>(dto);

        brand.CreatedAt = DateTime.UtcNow;

        await _brandRepository.AddAsync(brand);
        await _brandRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(BrandUpdateDto dto)
    {
        var brand = await _brandRepository.GetByIdAsync(dto.Id);

        if (brand == null)
            return;

        brand.Name = dto.Name;
        brand.Description = dto.Description;
        brand.UpdatedAt = DateTime.UtcNow;

        _brandRepository.Update(brand);

        await _brandRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var brand = await _brandRepository.GetByIdAsync(id);

        if (brand == null)
            return;

        _brandRepository.Delete(brand);

        await _brandRepository.SaveChangesAsync();
    }
}