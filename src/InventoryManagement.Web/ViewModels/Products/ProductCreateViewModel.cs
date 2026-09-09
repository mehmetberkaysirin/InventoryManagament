using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagement.Web.ViewModels.Products;

public class ProductCreateViewModel
{
    public string Name { get; set; } = string.Empty;

    public string? Barcode { get; set; }

    public string? Description { get; set; }

    public Guid CategoryId { get; set; }

    public Guid BrandId { get; set; }

    public Guid UnitId { get; set; }

    public List<SelectListItem> Categories { get; set; } = [];

    public List<SelectListItem> Brands { get; set; } = [];

    public List<SelectListItem> Units { get; set; } = [];
}