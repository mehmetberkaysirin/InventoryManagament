using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace InventoryManagement.Application.DTOs.Categories;

public class CategoryCreateDto
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
}