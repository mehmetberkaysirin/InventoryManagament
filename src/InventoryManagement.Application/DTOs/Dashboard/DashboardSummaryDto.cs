using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs.Dashboard
{
    public class DashboardSummaryDto
    {
        public int TotalProductCount { get; set; }
        public int TotalWarehouseCount { get; set; }
        public decimal TotalStockQuantity { get; set; }
        public int CriticalStockCount { get; set; }

        public List<MonthlyMovementDto> MonthlyMovements { get; set; } = new();
        public List<TopProductDto> TopProducts { get; set; } = new();
        public List<CriticalStockDto> CriticalStocks { get; set; } = new();
    }

    public class MonthlyMovementDto
    {
        public string MonthName { get; set; } = string.Empty;
        public decimal TotalIn { get; set; }
        public decimal TotalOut { get; set; }
    }

    public class TopProductDto
    {
        public string ProductName { get; set; } = string.Empty;
        public decimal TotalQuantity { get; set; }
    }

    public class CriticalStockDto
    {
        public Guid ProductId { get; set; } // Guid olarak güncellendi
        public string ProductName { get; set; } = string.Empty;
        public string WarehouseName { get; set; } = string.Empty;
        public decimal CurrentQuantity { get; set; }
        public decimal MinQuantity { get; set; }
    }
}