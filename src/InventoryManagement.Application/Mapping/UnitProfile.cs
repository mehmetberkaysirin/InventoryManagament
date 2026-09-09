using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using InventoryManagement.Application.DTOs.Units;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Mapping;

public class UnitProfile : Profile
{
    public UnitProfile()
    {
        CreateMap<UnitCreateDto, Unit>();
        CreateMap<UnitUpdateDto, Unit>();

        CreateMap<Unit, UnitListDto>();
        CreateMap<Unit, UnitDetailDto>();
    }
}