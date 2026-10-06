using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagement.Application.Interfaces.Repositories;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Repositories;

public class StockRepository : IStockRepository
{
    private readonly ApplicationDbContext _context;

    public StockRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Stock>> GetAllAsync()
    {
        return await _context.Stocks
            .Include(x => x.Product)     // Ürün adını getirmesi için şart
            .Include(x => x.Warehouse)   // Depo adını getirmesi için şart
            .Where(x => !x.IsDeleted)
            .ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(Guid id)
    {
        return await _context.Stocks
            .AsNoTracking() // <-- İŞTE BURASI! EF Core bu stoğu hafızasında takip etmesin.
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }

    public async Task AddAsync(Stock stock)
    {
        await _context.Stocks.AddAsync(stock);
    }

    public void Update(Stock stock)
    {
        _context.Stocks.Update(stock);
    }

    public async Task UpdateAsync(Stock stock)
    {
        // Standart EF Core tracking güncellemesi (ExecuteUpdateAsync yerine bu kullanılır)
        _context.Stocks.Update(stock);
        await Task.CompletedTask; // async imzasını korumak için
    }

    public void Delete(Stock stock)
    {
        stock.IsDeleted = true;
        _context.Stocks.Update(stock);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    public async Task UpdateQuantityAsync(Guid stockId, decimal newQuantity)
    {
        await _context.Stocks
            .Where(s => s.Id == stockId)
            .ExecuteUpdateAsync(s => s.SetProperty(st => st.Quantity, newQuantity));
    }
    public async Task<Stock?> GetByProductAndWarehouseAsync(Guid productId, Guid warehouseId)
    {
        return await _context.Stocks
            .FirstOrDefaultAsync(s => s.ProductId == productId && s.WarehouseId == warehouseId);
    }
}