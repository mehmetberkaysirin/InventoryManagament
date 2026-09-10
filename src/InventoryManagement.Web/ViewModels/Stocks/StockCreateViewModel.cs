using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagement.Web.ViewModels.Stocks;

public class StockCreateViewModel
{
    public Guid ProductId { get; set; }

    public Guid WarehouseId { get; set; }

    public decimal Quantity { get; set; }

    public List<SelectListItem> Products { get; set; } = [];

    public List<SelectListItem> Warehouses { get; set; } = [];
}
