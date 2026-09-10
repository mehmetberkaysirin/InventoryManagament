using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs.Dashboard;

public class DashboardDto
{
    public int TotalProducts { get; set; }

    public int TotalWarehouses { get; set; }

    public decimal TotalStockQuantity { get; set; }

    public int TotalStockMovements { get; set; }
}