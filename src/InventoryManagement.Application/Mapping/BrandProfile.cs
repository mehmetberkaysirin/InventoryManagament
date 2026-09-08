using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using InventoryManagement.Application.DTOs.Brands;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Mapping;

public class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<BrandCreateDto, Brand>();
        CreateMap<BrandUpdateDto, Brand>();
        CreateMap<Brand, BrandListDto>();
        CreateMap<Brand, BrandDetailDto>();
    }
}