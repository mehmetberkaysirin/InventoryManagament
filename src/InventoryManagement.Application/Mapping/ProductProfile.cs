using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using InventoryManagement.Application.DTOs.Products;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductCreateDto, Product>();
        CreateMap<ProductUpdateDto, Product>();

        CreateMap<Product, ProductListDto>()
            .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.BrandName,
                opt => opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.UnitName,
                opt => opt.MapFrom(src => src.Unit.Name));

        CreateMap<Product, ProductDetailDto>();
    }
}