using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InventoryManagement.Domain.Common;

namespace InventoryManagement.Domain.Entities;

public class Product : AuditableEntity
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string Barcode { get; set; } = string.Empty;

    public Guid CategoryId { get; set; }

    public Guid BrandId { get; set; }

    public Guid UnitId { get; set; }

    public int MinimumStockLevel { get; set; }

    public bool IsActive { get; set; } = true;

    public Category Category { get; set; } = null!;

    public Brand Brand { get; set; } = null!;

    public Unit Unit { get; set; } = null!;
}