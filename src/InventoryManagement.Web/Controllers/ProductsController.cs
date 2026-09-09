using InventoryManagement.Application.DTOs.Products;
using InventoryManagement.Application.Interfaces.Services;
using InventoryManagement.Web.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagement.Web.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IBrandService _brandService;
    private readonly IUnitService _unitService;

    public ProductsController(
        IProductService productService,
        ICategoryService categoryService,
        IBrandService brandService,
        IUnitService unitService)
    {
        _productService = productService;
        _categoryService = categoryService;
        _brandService = brandService;
        _unitService = unitService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllAsync();

        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = new ProductCreateViewModel();

        model.Categories = (await _categoryService.GetAllAsync())
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

        model.Brands = (await _brandService.GetAllAsync())
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

        model.Units = (await _unitService.GetAllAsync())
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateViewModel model)
    {
        var dto = new ProductCreateDto
        {
            Name = model.Name,
            Barcode = model.Barcode,
            Description = model.Description,
            CategoryId = model.CategoryId,
            BrandId = model.BrandId,
            UnitId = model.UnitId
        };

        await _productService.CreateAsync(dto);

        return RedirectToAction(nameof(Index));
    }
}