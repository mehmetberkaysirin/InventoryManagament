using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InventoryManagement.Domain.Common;

namespace InventoryManagement.Domain.Entities;

public class Unit : AuditableEntity
{
    public string Name { get; set; } = string.Empty;

    public string? ShortName { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
