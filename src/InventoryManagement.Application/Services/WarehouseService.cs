using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using InventoryManagement.Application.DTOs.Warehouses;
using InventoryManagement.Application.Interfaces.Repositories;
using InventoryManagement.Application.Interfaces.Services;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IMapper _mapper;

    public WarehouseService(
        IWarehouseRepository warehouseRepository,
        IMapper mapper)
    {
        _warehouseRepository = warehouseRepository;
        _mapper = mapper;
    }

    public async Task<List<WarehouseListDto>> GetAllAsync()
    {
        var warehouses = await _warehouseRepository.GetAllAsync();

        return _mapper.Map<List<WarehouseListDto>>(warehouses);
    }

    public async Task<WarehouseDetailDto?> GetByIdAsync(Guid id)
    {
        var warehouse = await _warehouseRepository.GetByIdAsync(id);

        if (warehouse == null)
            return null;

        return _mapper.Map<WarehouseDetailDto>(warehouse);
    }

    public async Task CreateAsync(WarehouseCreateDto dto)
    {
        var warehouse = _mapper.Map<Warehouse>(dto);

        warehouse.CreatedAt = DateTime.UtcNow;

        await _warehouseRepository.AddAsync(warehouse);
        await _warehouseRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(WarehouseUpdateDto dto)
    {
        var warehouse = await _warehouseRepository.GetByIdAsync(dto.Id);

        if (warehouse == null)
            return;

        warehouse.Name = dto.Name;
        warehouse.Code = dto.Code;
        warehouse.Description = dto.Description;
        warehouse.Address = dto.Address;
        warehouse.UpdatedAt = DateTime.UtcNow;

        _warehouseRepository.Update(warehouse);

        await _warehouseRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var warehouse = await _warehouseRepository.GetByIdAsync(id);

        if (warehouse == null)
            return;

        _warehouseRepository.Delete(warehouse);

        await _warehouseRepository.SaveChangesAsync();
    }
}