using InventoryManagement.Application.DTOs.Brands;
using InventoryManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Web.Controllers;

public class BrandsController : Controller
{
    private readonly IBrandService _brandService;

    public BrandsController(IBrandService brandService)
    {
        _brandService = brandService;
    }

    public async Task<IActionResult> Index()
    {
        var brands = await _brandService.GetAllAsync();

        return View(brands);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(BrandCreateDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        await _brandService.CreateAsync(dto);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var brand = await _brandService.GetByIdAsync(id);

        if (brand == null)
            return NotFound();

        var dto = new BrandUpdateDto
        {
            Id = brand.Id,
            Name = brand.Name,
            Description = brand.Description
        };

        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(BrandUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        await _brandService.UpdateAsync(dto);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _brandService.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}