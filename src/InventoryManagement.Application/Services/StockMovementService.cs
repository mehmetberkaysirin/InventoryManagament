using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using InventoryManagement.Application.DTOs.StockMovements;
using InventoryManagement.Application.Interfaces.Repositories;
using InventoryManagement.Application.Interfaces.Services;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;

namespace InventoryManagement.Application.Services;

public class StockMovementService : IStockMovementService
{
    private readonly IStockMovementRepository _stockMovementRepository;
    private readonly IStockRepository _stockRepository;
    private readonly IMapper _mapper;

    public StockMovementService(
        IStockMovementRepository stockMovementRepository,
        IStockRepository stockRepository,
        IMapper mapper)
    {
        _stockMovementRepository = stockMovementRepository;
        _stockRepository = stockRepository;
        _mapper = mapper;
    }

    public async Task<List<StockMovementListDto>> GetAllAsync()
    {
        var movements = await _stockMovementRepository.GetAllAsync();

        return _mapper.Map<List<StockMovementListDto>>(movements);
    }

    public async Task CreateAsync(StockMovementCreateDto dto)
    {
        var stock = await _stockRepository.GetByIdAsync(dto.StockId);

        if (stock == null)
            throw new Exception("Stok kaydı bulunamadı.");

        switch (dto.MovementType)
        {
            case StockMovementType.Entry:
                stock.Quantity += dto.Quantity;
                break;

            case StockMovementType.TransferIn:
                stock.Quantity += dto.Quantity;
                break;

            case StockMovementType.Exit:

                if (stock.Quantity < dto.Quantity)
                    throw new Exception("Yetersiz stok.");

                stock.Quantity -= dto.Quantity;
                break;

            case StockMovementType.TransferOut:

                if (stock.Quantity < dto.Quantity)
                    throw new Exception("Yetersiz stok.");

                stock.Quantity -= dto.Quantity;
                break;

            case StockMovementType.CountAdjustment:
                stock.Quantity = dto.Quantity;
                break;
        }

        _stockRepository.Update(stock);

        var movement = new StockMovement
        {
            StockId = dto.StockId,
            Quantity = dto.Quantity,
            MovementType = dto.MovementType,
            Description = dto.Description,
            CreatedAt = DateTime.UtcNow
        };

        await _stockMovementRepository.AddAsync(movement);

        await _stockRepository.SaveChangesAsync();
        await _stockMovementRepository.SaveChangesAsync();
    }
}