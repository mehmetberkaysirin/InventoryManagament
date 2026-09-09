using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using InventoryManagement.Application.DTOs.Warehouses;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Mapping;

public class WarehouseProfile : Profile
{
    public WarehouseProfile()
    {
        CreateMap<WarehouseCreateDto, Warehouse>();
        CreateMap<WarehouseUpdateDto, Warehouse>();

        CreateMap<Warehouse, WarehouseListDto>();
        CreateMap<Warehouse, WarehouseDetailDto>();
    }
}