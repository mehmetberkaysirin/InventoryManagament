using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InventoryManagement.Domain.Enums;

namespace InventoryManagement.Application.DTOs.StockMovements;

public class StockMovementCreateDto
{
    public Guid StockId { get; set; }

    public decimal Quantity { get; set; }

    public StockMovementType MovementType { get; set; }

    public string? Description { get; set; }
}