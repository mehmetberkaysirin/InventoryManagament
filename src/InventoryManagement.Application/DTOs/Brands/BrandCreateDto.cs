using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs.Brands;

public class BrandCreateDto
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
}