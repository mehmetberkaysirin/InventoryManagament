using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs.StockMovements;

public class StockMovementDetailDto
{
    public Guid Id { get; set; }

    public Guid StockId { get; set; }

    public decimal Quantity { get; set; }

    public string MovementType { get; set; } = string.Empty;

    public string? Description { get; set; }
}