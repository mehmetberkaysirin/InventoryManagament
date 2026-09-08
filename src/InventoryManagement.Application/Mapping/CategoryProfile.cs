using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using InventoryManagement.Application.DTOs.Categories;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Mapping;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryListDto>();

        CreateMap<Category, CategoryDetailDto>();

        CreateMap<CategoryCreateDto, Category>();

        CreateMap<CategoryUpdateDto, Category>();
    }
}