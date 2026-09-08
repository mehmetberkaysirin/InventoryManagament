using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InventoryManagement.Domain.Common;
using InventoryManagement.Domain.Enums;

namespace InventoryManagement.Domain.Entities;

public class StockMovement : AuditableEntity
{
    public Guid StockId { get; set; }

    public StockMovementType MovementType { get; set; }

    public decimal Quantity { get; set; }

    public string? Description { get; set; }

    public Guid? ReferenceId { get; set; }

    public Stock Stock { get; set; } = null!;
}