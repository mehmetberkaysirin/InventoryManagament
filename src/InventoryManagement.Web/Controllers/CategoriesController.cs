using InventoryManagement.Application.DTOs.Categories;
using InventoryManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Web.Controllers;

[Authorize]
public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllAsync();

        return View(categories);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryCreateDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        await _categoryService.CreateAsync(dto);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var category = await _categoryService.GetByIdAsync(id);

        if (category == null)
            return NotFound();

        var dto = new CategoryUpdateDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };

        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CategoryUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        await _categoryService.UpdateAsync(dto);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _categoryService.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}