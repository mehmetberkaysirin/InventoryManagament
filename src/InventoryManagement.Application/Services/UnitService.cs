using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using InventoryManagement.Application.DTOs.Units;
using InventoryManagement.Application.Interfaces.Repositories;
using InventoryManagement.Application.Interfaces.Services;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Services;

public class UnitService : IUnitService
{
    private readonly IUnitRepository _unitRepository;
    private readonly IMapper _mapper;

    public UnitService(
        IUnitRepository unitRepository,
        IMapper mapper)
    {
        _unitRepository = unitRepository;
        _mapper = mapper;
    }

    public async Task<List<UnitListDto>> GetAllAsync()
    {
        var units = await _unitRepository.GetAllAsync();

        return _mapper.Map<List<UnitListDto>>(units);
    }

    public async Task<UnitDetailDto?> GetByIdAsync(Guid id)
    {
        var unit = await _unitRepository.GetByIdAsync(id);

        if (unit == null)
            return null;

        return _mapper.Map<UnitDetailDto>(unit);
    }

    public async Task CreateAsync(UnitCreateDto dto)
    {
        var unit = _mapper.Map<Unit>(dto);

        unit.CreatedAt = DateTime.UtcNow;

        await _unitRepository.AddAsync(unit);
        await _unitRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(UnitUpdateDto dto)
    {
        var unit = await _unitRepository.GetByIdAsync(dto.Id);

        if (unit == null)
            return;

        unit.Name = dto.Name;
        unit.ShortName = dto.ShortName;
        unit.UpdatedAt = DateTime.UtcNow;

        _unitRepository.Update(unit);

        await _unitRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var unit = await _unitRepository.GetByIdAsync(id);

        if (unit == null)
            return;

        _unitRepository.Delete(unit);

        await _unitRepository.SaveChangesAsync();
    }
}