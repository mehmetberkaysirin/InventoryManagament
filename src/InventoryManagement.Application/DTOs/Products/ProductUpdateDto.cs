using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs.Products;

public class ProductUpdateDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Barcode { get; set; }

    public string? Description { get; set; }

    public Guid CategoryId { get; set; }

    public Guid BrandId { get; set; }

    public Guid UnitId { get; set; }
}
