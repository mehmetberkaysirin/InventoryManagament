using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs.Products;

public class ProductListDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Barcode { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public string BrandName { get; set; } = string.Empty;

    public string UnitName { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}