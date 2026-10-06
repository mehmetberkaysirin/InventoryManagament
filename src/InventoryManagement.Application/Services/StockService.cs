using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using InventoryManagement.Application.DTOs.Stocks;
using InventoryManagement.Application.Interfaces.Repositories;
using InventoryManagement.Application.Interfaces.Services;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Services;

public class StockService : IStockService
{
    private readonly IStockRepository _stockRepository;
    private readonly IMapper _mapper;


    public StockService(
        IStockRepository stockRepository,
        IMapper mapper)
    {
        _stockRepository = stockRepository;
        _mapper = mapper;
    }

    public async Task<List<StockListDto>> GetAllAsync()
    {
        var stocks = await _stockRepository.GetAllAsync();

        return _mapper.Map<List<StockListDto>>(stocks);
    }

    public async Task<StockDetailDto?> GetByIdAsync(Guid id)
    {
        var stock = await _stockRepository.GetByIdAsync(id);

        if (stock == null)
            return null;

        return _mapper.Map<StockDetailDto>(stock);
    }

    public async Task CreateAsync(StockCreateDto dto)
    {
        // 1. Mevcut stokları çekip LINQ ile bu ürün ve depo kombinasyonu var mı diye bakıyoruz
        var allStocks = await _stockRepository.GetAllAsync();
        var existingStock = allStocks.FirstOrDefault(s => s.ProductId == dto.ProductId && s.WarehouseId == dto.WarehouseId);

        if (existingStock != null)
        {
            // 2A. ZATEN VARSA: Miktarını güncelliyoruz (Update)
            existingStock.Quantity += dto.Quantity;
            existingStock.UpdatedAt = DateTime.UtcNow;

            _stockRepository.Update(existingStock);
        }
        else
        {
            // 2B. YOKSA: Sıfırdan ekliyoruz (Insert)
            var stock = _mapper.Map<Stock>(dto);
            stock.Id = Guid.NewGuid();
            stock.CreatedAt = DateTime.UtcNow;

            await _stockRepository.AddAsync(stock);
        }

        // Değişiklikleri kaydediyoruz, böylece SignalR ve ekran patlamadan çalışacak
        await _stockRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(StockUpdateDto dto)
    {
        var stock = await _stockRepository.GetByIdAsync(dto.Id);

        if (stock == null)
            return;

        stock.ProductId = dto.ProductId;
        stock.WarehouseId = dto.WarehouseId;
        stock.Quantity = dto.Quantity;
        stock.UpdatedAt = DateTime.UtcNow;

        _stockRepository.Update(stock);

        await _stockRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var stock = await _stockRepository.GetByIdAsync(id);

        if (stock == null)
            return;

        _stockRepository.Delete(stock);

        await _stockRepository.SaveChangesAsync();
    }
    public async Task<Stock?> GetByProductAndWarehouseAsync(Guid productId, Guid warehouseId)
    {
        var allStocks = await _stockRepository.GetAllAsync();
        return allStocks.FirstOrDefault(s => s.ProductId == productId && s.WarehouseId == warehouseId);
    }
}