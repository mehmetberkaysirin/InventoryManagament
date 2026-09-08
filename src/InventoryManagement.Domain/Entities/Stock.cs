using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InventoryManagement.Domain.Common;

namespace InventoryManagement.Domain.Entities;

public class Stock : AuditableEntity
{
    public Guid ProductId { get; set; }

    public Guid WarehouseId { get; set; }

    public decimal Quantity { get; set; }

    public Product Product { get; set; } = null!;

    public Warehouse Warehouse { get; set; } = null!;

    public ICollection<StockMovement> Movements { get; set; }
        = new List<StockMovement>();
}
