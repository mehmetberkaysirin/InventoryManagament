using InventoryManagement.Domain.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Domain.Entities;

public class Warehouse : AuditableEntity
{
    public string Name { get; set; } = string.Empty;

    public string? Code { get; set; }

    public string? Description { get; set; }

    public string? Address { get; set; }

  

    public ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}