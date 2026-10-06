using System;
using System.Collections.Generic;
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
    private readonly IStockNotifier _stockNotifier;
    private readonly IMapper _mapper;

    public StockMovementService(
        IStockMovementRepository stockMovementRepository,
        IStockRepository stockRepository,
        IStockNotifier stockNotifier,
        IMapper mapper)
    {
        _stockMovementRepository = stockMovementRepository;
        _stockRepository = stockRepository;
        _stockNotifier = stockNotifier;
        _mapper = mapper;
    }

    public async Task<List<StockMovementListDto>> GetAllAsync()
    {
        var movements = await _stockMovementRepository.GetAllAsync();
        return _mapper.Map<List<StockMovementListDto>>(movements);
    }

    public async Task CreateAsync(StockMovementCreateDto dto)
    {
        // 1. Stoğu bul ve miktarını güncelle
        var stock = await _stockRepository.GetByIdAsync(dto.StockId);
        if (stock == null)
            throw new Exception($"ID'si {dto.StockId} olan stok veritabanında bulunamadı!");

        decimal newQuantity = stock.Quantity;
        switch (dto.MovementType)
        {
            case StockMovementType.Entry:
            case StockMovementType.TransferIn:
                newQuantity += dto.Quantity;
                break;
            case StockMovementType.Exit:
            case StockMovementType.TransferOut:
                if (stock.Quantity < dto.Quantity)
                    throw new Exception("Yetersiz stok.");
                newQuantity -= dto.Quantity;
                break;
            case StockMovementType.CountAdjustment:
                newQuantity = dto.Quantity;
                break;
        }

        // --- EN KRİTİK NOKTA BURASI ---
        // Doğrudan SQL ile güncelleyerek EF Core ChangeTracker'ın hafızasını ve 
        // stock nesnesini kirletmesini yüzde yüz engelliyoruz.
        await _stockRepository.UpdateQuantityAsync(dto.StockId, newQuantity);

        // 2. Şimdi stok hareketini tertemiz ve bağımsız bir şekilde ekliyoruz.
        var movement = new StockMovement
        {
            Id = Guid.NewGuid(),
            StockId = dto.StockId,
            Quantity = dto.Quantity,
            MovementType = dto.MovementType,
            Description = dto.Description,
            CreatedAt = DateTime.UtcNow
        };

        await _stockMovementRepository.AddAsync(movement);
        await _stockMovementRepository.SaveChangesAsync();

        // Stok hareketi başarıyla veritabanına işlendikten sonra SignalR ile tetikleme yapılıyor
        await _stockNotifier.NotifyStockUpdatedAsync();
    }
}