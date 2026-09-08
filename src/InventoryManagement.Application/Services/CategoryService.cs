using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using InventoryManagement.Application.DTOs.Categories;
using InventoryManagement.Application.Interfaces.Services;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Application.Interfaces.Repositories;

namespace InventoryManagement.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(
        ICategoryRepository categoryRepository,
        IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<List<CategoryListDto>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();

        return _mapper.Map<List<CategoryListDto>>(categories);
    }

    public async Task<CategoryDetailDto?> GetByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category is null)
            return null;

        return _mapper.Map<CategoryDetailDto>(category);
    }

    public async Task CreateAsync(CategoryCreateDto dto)
    {
        var category = _mapper.Map<Category>(dto);

        category.CreatedAt = DateTime.UtcNow;

        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(CategoryUpdateDto dto)
    {
        var category = await _categoryRepository.GetByIdAsync(dto.Id);

        if (category is null)
            return;

        category.Name = dto.Name;
        category.Description = dto.Description;
        category.UpdatedAt = DateTime.UtcNow;

        _categoryRepository.Update(category);

        await _categoryRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category is null)
            return;

        _categoryRepository.Delete(category);

        await _categoryRepository.SaveChangesAsync();
    }
}