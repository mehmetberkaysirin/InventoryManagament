using InventoryManagement.Infrastructure.Data;
using InventoryManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using InventoryManagement.Application.Mapping;
using InventoryManagement.Application.Interfaces.Repositories;
using InventoryManagement.Application.Interfaces.Services;
using InventoryManagement.Application.Services;
using InventoryManagement.Infrastructure.Repositories;
using InventoryManagement.Web.Hubs;

var builder = WebApplication.CreateBuilder(args);

// DbContext Kaydı
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// ASP.NET Core Identity Kaydı
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Cookie (Oturum) Yapılandırması
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.Cookie.Name = "InventoryManagement.Auth";
    options.ExpireTimeSpan = TimeSpan.FromHours(8);
    options.SlidingExpiration = true;
});

// Services & Repositories
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IUnitRepository, UnitRepository>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IStockMovementRepository, StockMovementRepository>();
builder.Services.AddScoped<IStockMovementService, StockMovementService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<CategoryProfile>();
    cfg.AddProfile<BrandProfile>();
    cfg.AddProfile<UnitProfile>();
    cfg.AddProfile<WarehouseProfile>();
    cfg.AddProfile<ProductProfile>();
    cfg.AddProfile<StockProfile>();
    cfg.AddProfile<StockMovementProfile>();
});

builder.Services.AddSignalR();

var app = builder.Build();

// 1. SEED DATA TETİKLEME (Uygulama ayağa kalkarken verileri basar)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await DataSeeder.SeedRolesAndAdminAsync(services);

        var context = services.GetRequiredService<ApplicationDbContext>();
        await DataSeeder.SeedBusinessDataAsync(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Seed data yüklenirken bir hata oluştu.");
    }
}

// 2. HTTP REQUEST PIPELINE (Middleware Sıralaması)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Veya app.MapStaticAssets();

app.UseRouting();

// Authentication HER ZAMAN Authorization'dan ÖNCE GELMELİDİR
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// SignalR Hub Endpoint Tanımı
app.MapHub<StockHub>("/stockHub");

// Tek ve doğru app.Run() en sonda!
app.Run();