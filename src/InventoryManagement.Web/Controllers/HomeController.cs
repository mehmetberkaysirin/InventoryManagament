using InventoryManagement.Application.Interfaces.Services;
using InventoryManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InventoryManagement.Web.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IDashboardService _dashboardService;

    public HomeController(ILogger<HomeController> logger, IDashboardService dashboardService)
    {
        _logger = logger;
        _dashboardService = dashboardService;
    }

    public async Task<IActionResult> Index()
    {
        var summary = await _dashboardService.GetDashboardSummaryAsync();
        return View(summary);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}