using InventoryManagement.Application.DTOs.StockMovements;
using InventoryManagement.Application.Interfaces.Services;
using InventoryManagement.Web.Hubs;
using InventoryManagement.Web.ViewModels.StockMovements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagement.Web.Controllers;

[Authorize]
public class StockMovementsController : Controller
{
    private readonly IStockMovementService _stockMovementService;
    private readonly IStockService _stockService;
    private readonly IHubContext<StockHub> _hubContext;

    public StockMovementsController(
        IStockMovementService stockMovementService,
        IStockService stockService,
        IHubContext<StockHub> hubContext)
    {
        _stockMovementService = stockMovementService;
        _stockService = stockService;
        _hubContext = hubContext;
    }

    public async Task<IActionResult> Index()
    {
        var movements = await _stockMovementService.GetAllAsync();
        return View(movements);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = new StockMovementCreateViewModel();

        model.Stocks = (await _stockService.GetAllAsync())
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = $"{x.ProductName} - {x.WarehouseName}"
            })
            .ToList();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(StockMovementCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Stocks = (await _stockService.GetAllAsync())
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.ProductName} - {x.WarehouseName}"
                })
                .ToList();

            return View(model);
        }

        var dto = new StockMovementCreateDto
        {
            StockId = model.StockId,
            Quantity = model.Quantity,
            MovementType = model.MovementType,
            Description = model.Description
        };

        await _stockMovementService.CreateAsync(dto);

        // Stok hareketi eklendiğinde anlık olarak SignalR tetiklenir
        await _hubContext.Clients.All.SendAsync("ReceiveStockMovementUpdate");

        return RedirectToAction(nameof(Index));
    }
}