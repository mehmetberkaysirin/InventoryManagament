using InventoryManagement.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagement.Web.ViewModels.StockMovements;

public class StockMovementCreateViewModel
{
    public Guid StockId { get; set; }

    public decimal Quantity { get; set; }

    public StockMovementType MovementType { get; set; }

    public string? Description { get; set; }

    public List<SelectListItem> Stocks { get; set; } = [];
}