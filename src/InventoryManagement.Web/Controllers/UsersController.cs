using InventoryManagement.Infrastructure.Identity;
using InventoryManagement.Web.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Web.Controllers;

[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UsersController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users
    .Select(u => new UserListViewModel
    {
        Id = u.Id.ToString(), // Guid değerini string'e dönüştürüyoruz
        FirstName = u.FirstName,
        LastName = u.LastName,
        Email = u.Email!,
        IsActive = u.IsActive
    })
    .ToListAsync();

        return View(users);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateUserViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var existingUser = await _userManager.FindByEmailAsync(model.Email);
        if (existingUser != null)
        {
            ModelState.AddModelError("Email", "Bu e-posta adresi zaten kullanılmakta.");
            return View(model);
        }

        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            IsActive = model.IsActive,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            TempData["SuccessMessage"] = "Kullanıcı başarıyla oluşturuldu.";
            return RedirectToAction(nameof(Index));
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleStatus(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound();

        user.IsActive = !user.IsActive;
        await _userManager.UpdateAsync(user);

        TempData["SuccessMessage"] = $"Kullanıcı durumu {(user.IsActive ? "Aktif" : "Pasif")} olarak güncellendi.";
        return RedirectToAction(nameof(Index));
    }
    // GET: /Users/ResetPassword/id
    [HttpGet]
    public async Task<IActionResult> ResetPassword(string id)
    {
        if (string.IsNullOrEmpty(id))
            return NotFound();

        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound();

        var model = new AdminResetPasswordViewModel
        {
            UserId = user.Id.ToString(),
            UserName = user.UserName ?? user.Email ?? string.Empty
        };

        return View(model); // Veya Modal/PartialView
    }

    // POST: /Users/ResetPassword
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(AdminResetPasswordViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null)
            return NotFound();

        // Eski şifreyi bilmeden sıfırlamak için Token üretiyoruz
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

        if (result.Succeeded)
        {
            TempData["SuccessMessage"] = $"{user.UserName} kullanıcısının şifresi başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }
}