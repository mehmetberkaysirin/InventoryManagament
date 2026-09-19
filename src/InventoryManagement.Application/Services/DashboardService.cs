using InventoryManagement.Application.DTOs.Dashboard;
using InventoryManagement.Application.Interfaces.Repositories;
using InventoryManagement.Application.Interfaces.Services;
using InventoryManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IProductRepository _productRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IStockMovementRepository _stockMovementRepository;

        public DashboardService(
            IProductRepository productRepository,
            IWarehouseRepository warehouseRepository,
            IStockRepository stockRepository,
            IStockMovementRepository stockMovementRepository)
        {
            _productRepository = productRepository;
            _warehouseRepository = warehouseRepository;
            _stockRepository = stockRepository;
            _stockMovementRepository = stockMovementRepository;
        }

        public async Task<DashboardSummaryDto> GetDashboardSummaryAsync()
        {
            var summary = new DashboardSummaryDto();

            // Tüm Ürünler ve Depolar
            var products = await _productRepository.GetAllAsync();
            var warehouses = await _warehouseRepository.GetAllAsync();

            summary.TotalProductCount = products.Count(p => p.IsActive);
            summary.TotalWarehouseCount = warehouses.Count(w => w.IsActive);

            var stocks = await _stockRepository.GetAllAsync();
            summary.TotalStockQuantity = stocks.Sum(s => s.Quantity);

            // Kritik Stoklar (Mevcut miktar <= 10)
            var criticalStocksList = stocks
                .Where(s => s.Quantity <= 10)
                .Take(5)
                .Select(s => new CriticalStockDto
                {
                    ProductId = s.ProductId,
                    ProductName = s.Product?.Name ?? "Bilinmiyor",
                    WarehouseName = s.Warehouse?.Name ?? "Bilinmiyor",
                    CurrentQuantity = s.Quantity,
                    MinQuantity = 10
                }).ToList();

            summary.CriticalStocks = criticalStocksList;
            summary.CriticalStockCount = criticalStocksList.Count;

            // Son 6 Ayın Hareket Grafiği Verisi
            var movements = await _stockMovementRepository.GetAllAsync();
            var last6Months = Enumerable.Range(0, 6)
                .Select(i => DateTime.Now.AddMonths(-i))
                .OrderBy(d => d)
                .ToList();

            foreach (var date in last6Months)
            {
                var monthMovements = movements.Where(m => m.CreatedAt.Month == date.Month && m.CreatedAt.Year == date.Year);

                // Giriş Türleri (Entry, TransferIn)
                var totalIn = monthMovements
                    .Where(m => m.MovementType == StockMovementType.Entry || m.MovementType == StockMovementType.TransferIn)
                    .Sum(m => m.Quantity);

                // Çıkış Türleri (Exit, TransferOut)
                var totalOut = monthMovements
                    .Where(m => m.MovementType == StockMovementType.Exit || m.MovementType == StockMovementType.TransferOut)
                    .Sum(m => Math.Abs(m.Quantity));

                summary.MonthlyMovements.Add(new MonthlyMovementDto
                {
                    MonthName = date.ToString("MMM yyyy"),
                    TotalIn = totalIn,
                    TotalOut = totalOut
                });
            }

            return summary;
        }
    }
}