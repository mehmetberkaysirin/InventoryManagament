using Bogus;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;
using InventoryManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Infrastructure.Data;

public static class DataSeeder
{
    // 1. IDENTITY ROLLER VE ADMIN (Kullanıcılar asla silinmez, güvende!)
    public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        string[] roles = { "Admin", "WarehouseManager", "User", "Depo Görevlisi" };

        foreach (var roleName in roles)
        {
            var roleExists = await roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = roleName });
            }
        }

        var adminEmail = "admin@inventory.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            var newAdmin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = "Sistem",
                LastName = "Yöneticisi",
                EmailConfirmed = true,
                IsActive = true
            };

            var result = await userManager.CreateAsync(newAdmin, "Admin123!*");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(newAdmin, "Admin");
            }
        }
    }

    // 2. STOK VE İŞ VERİLERİ (BOGUS)
    public static async Task SeedBusinessDataAsync(ApplicationDbContext context)
    {
        // Eğer veritabanında zaten ürün varsa, tekrar basıp çakışma yaratmasın
        if (await context.Products.AnyAsync())
        {
            return;
        }

        var units = new List<Unit>
        {
            new() { Name = "Adet" },
            new() { Name = "Metre" },
            new() { Name = "Kutu" },
            new() { Name = "Paket" }
        };
        await context.Units.AddRangeAsync(units);

        var categories = new List<Category>
        {
            new() { Name = "Ağ & Network Ekipmanları", Description = "Switch, Router ve Kablolama" },
            new() { Name = "Bilgisayar & Parça", Description = "Masaüstü, Laptop ve İç Donanımlar" },
            new() { Name = "Çevre Bilimleri & Aksesuar", Description = "Klavye, Mouse, Monitör" },
            new() { Name = "Güç & Kesintisiz Güç Kaynağı", Description = "UPS ve Power Kabloları" }
        };
        await context.Categories.AddRangeAsync(categories);

        var brands = new List<Brand>
        {
            new() { Name = "Cisco" },
            new() { Name = "Dell" },
            new() { Name = "HP" },
            new() { Name = "Logitech" }
        };
        await context.Brands.AddRangeAsync(brands);

        var warehouses = new List<Warehouse>
        {
            new() { Name = "Ana Depo" },
            new() { Name = "Ankara Lojistik Depo" },
            new() { Name = "İzmir Bölge Deposu" }
        };
        await context.Warehouses.AddRangeAsync(warehouses);

        await context.SaveChangesAsync();

        var categoriesList = await context.Categories.ToListAsync();
        var brandsList = await context.Brands.ToListAsync();
        var unitsList = await context.Units.ToListAsync();
        var warehousesList = await context.Warehouses.ToListAsync();

        var random = new Random();

        var productFaker = new Faker<Product>("tr")
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Barcode, f => f.Commerce.Ean13())
            .RuleFor(p => p.MinimumStockLevel, f => f.Random.Number(5, 20))
            .RuleFor(p => p.CategoryId, f => f.PickRandom(categoriesList).Id)
            .RuleFor(p => p.BrandId, f => f.PickRandom(brandsList).Id)
            .RuleFor(p => p.UnitId, f => f.PickRandom(unitsList).Id)
            // Eğer BaseEntity/AuditableEntity'de CreatedAt varsa burası patlamasın diye tarih veriyoruz:
            .RuleFor(p => p.CreatedAt, f => f.Date.Past(1));

        var products = productFaker.Generate(30);
        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();

        var stockList = new List<Stock>();

        foreach (var product in products)
        {
            foreach (var warehouse in warehousesList)
            {
                stockList.Add(new Stock
                {
                    ProductId = product.Id,
                    WarehouseId = warehouse.Id,
                    Quantity = random.Next(5, 150),
                    CreatedAt = DateTime.UtcNow.AddDays(-random.Next(1, 30))
                });
            }
        }

        await context.Stocks.AddRangeAsync(stockList);
        await context.SaveChangesAsync();

        // STOK HAREKETLERİ (StockId uyumlu ve Tarihleri düzeltilmiş)
        var stockMovementList = new List<StockMovement>();

        foreach (var stock in stockList)
        {
            for (int i = 0; i < random.Next(2, 5); i++)
            {
                bool isEntry = random.Next(0, 2) == 0;
                decimal moveQty = random.Next(5, 25);

                stockMovementList.Add(new StockMovement
                {
                    StockId = stock.Id,
                    Quantity = moveQty,
                    MovementType = isEntry ? StockMovementType.Entry : StockMovementType.Exit,
                    Description = isEntry ? "Tedarikçi Alım Girişi" : "Müşteri Sipariş Çıkışı",
                    // Tarih sıfırlanma (0001) sorununu kökten çözen nokta:
                    CreatedAt = DateTime.UtcNow.AddDays(-random.Next(1, 20))
                });
            }
        }

        await context.StockMovements.AddRangeAsync(stockMovementList);
        await context.SaveChangesAsync();
    }
}