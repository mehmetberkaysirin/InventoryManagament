using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Web.Controllers;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}