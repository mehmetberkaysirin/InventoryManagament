using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs.Stocks;

public class StockListDto
{
    public Guid Id { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public string WarehouseName { get; set; } = string.Empty;

    public decimal Quantity { get; set; }
}
