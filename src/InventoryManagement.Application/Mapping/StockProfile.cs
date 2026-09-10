using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using InventoryManagement.Application.DTOs.Stocks;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Mapping;

public class StockProfile : Profile
{
    public StockProfile()
    {
        CreateMap<StockCreateDto, Stock>();

        CreateMap<StockUpdateDto, Stock>();

        CreateMap<Stock, StockListDto>()
            .ForMember(dest => dest.ProductName,
                opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.WarehouseName,
                opt => opt.MapFrom(src => src.Warehouse.Name));

        CreateMap<Stock, StockDetailDto>();
    }
}