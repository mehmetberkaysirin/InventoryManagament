using InventoryManagement.Infrastructure.Identity;
using InventoryManagement.Web.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Web.Controllers;

[Authorize(Roles = "Admin")]
public class RolesController : Controller
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public RolesController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var roles = await _roleManager.Roles
            .Select(r => new RoleViewModel
            {
                Id = r.Id,
                Name = r.Name!
            })
            .ToListAsync();

        return View(roles);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RoleViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var roleExists = await _roleManager.RoleExistsAsync(model.Name);
        if (roleExists)
        {
            ModelState.AddModelError("Name", "Bu rol zaten mevcut.");
            return View(model);
        }

        var result = await _roleManager.CreateAsync(new ApplicationRole { Name = model.Name });

        if (result.Succeeded)
        {
            TempData["SuccessMessage"] = "Rol başarıyla oluşturuldu.";
            return RedirectToAction(nameof(Index));
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> AssignRole(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            return NotFound();

        var roles = await _roleManager.Roles.ToListAsync();
        var userRoles = await _userManager.GetRolesAsync(user);

        var model = new AssignRoleViewModel
        {
            UserId = user.Id,
            UserFullName = $"{user.FirstName} {user.LastName}",
            Roles = roles.Select(r => new RoleAssignItem
            {
                RoleId = r.Id,
                RoleName = r.Name!,
                IsExist = userRoles.Contains(r.Name!)
            }).ToList()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AssignRole(AssignRoleViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId.ToString());
        if (user == null)
            return NotFound();

        foreach (var item in model.Roles)
        {
            if (item.IsExist)
            {
                await _userManager.AddToRoleAsync(user, item.RoleName);
            }
            else
            {
                await _userManager.RemoveFromRoleAsync(user, item.RoleName);
            }
        }

        TempData["SuccessMessage"] = $"{user.FirstName} {user.LastName} için rol güncellemesi tamamlandı.";
        return RedirectToAction("Index", "Users");
    }
}