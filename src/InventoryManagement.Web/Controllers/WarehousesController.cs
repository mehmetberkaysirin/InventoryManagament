using InventoryManagement.Application.DTOs.Warehouses;
using InventoryManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Web.Controllers;

[Authorize]
public class WarehousesController : Controller
{
    private readonly IWarehouseService _warehouseService;

    public WarehousesController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    public async Task<IActionResult> Index()
    {
        var warehouses = await _warehouseService.GetAllAsync();

        return View(warehouses);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(WarehouseCreateDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        await _warehouseService.CreateAsync(dto);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var warehouse = await _warehouseService.GetByIdAsync(id);

        if (warehouse == null)
            return NotFound();

        var dto = new WarehouseUpdateDto
        {
            Id = warehouse.Id,
            Name = warehouse.Name,
            Code = warehouse.Code,
            Description = warehouse.Description,
            Address = warehouse.Address
        };

        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(WarehouseUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        await _warehouseService.UpdateAsync(dto);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _warehouseService.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}