using InventoryManagement.Application.DTOs.Stocks;
using InventoryManagement.Application.Interfaces.Services;
using InventoryManagement.Web.ViewModels.Stocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagement.Web.Controllers;

public class StocksController : Controller
{
    private readonly IStockService _stockService;
    private readonly IProductService _productService;
    private readonly IWarehouseService _warehouseService;

    public StocksController(
        IStockService stockService,
        IProductService productService,
        IWarehouseService warehouseService)
    {
        _stockService = stockService;
        _productService = productService;
        _warehouseService = warehouseService;
    }

    public async Task<IActionResult> Index()
    {
        var stocks = await _stockService.GetAllAsync();

        return View(stocks);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = new StockCreateViewModel();

        model.Products = (await _productService.GetAllAsync())
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

        model.Warehouses = (await _warehouseService.GetAllAsync())
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(StockCreateViewModel model)
    {
        var dto = new StockCreateDto
        {
            ProductId = model.ProductId,
            WarehouseId = model.WarehouseId,
            Quantity = model.Quantity
        };

        await _stockService.CreateAsync(dto);

        return RedirectToAction(nameof(Index));
    }
}