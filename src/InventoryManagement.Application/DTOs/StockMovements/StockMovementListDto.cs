using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs.StockMovements;

public class StockMovementListDto
{
    public Guid Id { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public string WarehouseName { get; set; } = string.Empty;

    public decimal Quantity { get; set; }

    public string MovementType { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
