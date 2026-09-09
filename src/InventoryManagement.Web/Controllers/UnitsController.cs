using InventoryManagement.Application.DTOs.Units;
using InventoryManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Web.Controllers;

public class UnitsController : Controller
{
    private readonly IUnitService _unitService;

    public UnitsController(IUnitService unitService)
    {
        _unitService = unitService;
    }

    public async Task<IActionResult> Index()
    {
        var units = await _unitService.GetAllAsync();

        return View(units);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(UnitCreateDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        await _unitService.CreateAsync(dto);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var unit = await _unitService.GetByIdAsync(id);

        if (unit == null)
            return NotFound();

        var dto = new UnitUpdateDto
        {
            Id = unit.Id,
            Name = unit.Name,
            ShortName = unit.ShortName
        };

        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UnitUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        await _unitService.UpdateAsync(dto);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _unitService.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}