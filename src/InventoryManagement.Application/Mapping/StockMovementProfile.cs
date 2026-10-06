using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using InventoryManagement.Application.DTOs.StockMovements;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Mapping;

public class StockMovementProfile : Profile
{
    public StockMovementProfile()
    {
        CreateMap<StockMovementCreateDto, StockMovement>()
            .ForMember(dest => dest.Stock, opt => opt.Ignore()); // <-- İŞTE BURASI! AutoMapper'ın Stock nesnesine dokunmasını kesin olarak yasaklıyoruz.

        CreateMap<StockMovement, StockMovementListDto>()
            .ForMember(dest => dest.ProductName,
                opt => opt.MapFrom(src => src.Stock.Product.Name))
            .ForMember(dest => dest.WarehouseName,
                opt => opt.MapFrom(src => src.Stock.Warehouse.Name));

        CreateMap<StockMovement, StockMovementDetailDto>();
    }
}
