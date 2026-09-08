using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Domain.Common;

namespace InventoryManagement.Domain.Entities;

public class Brand : AuditableEntity
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

 
    public ICollection<Product> Products { get; set; } = new List<Product>();
}