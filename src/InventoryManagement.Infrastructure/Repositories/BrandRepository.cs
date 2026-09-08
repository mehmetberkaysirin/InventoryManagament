using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InventoryManagement.Application.Interfaces.Repositories;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly ApplicationDbContext _context;

    public BrandRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Brand>> GetAllAsync()
    {
        return await _context.Brands
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .ToListAsync();
    }

    public async Task<Brand?> GetByIdAsync(Guid id)
    {
        return await _context.Brands
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }

    public async Task AddAsync(Brand brand)
    {
        await _context.Brands.AddAsync(brand);
    }

    public void Update(Brand brand)
    {
        _context.Brands.Update(brand);
    }

    public void Delete(Brand brand)
    {
        brand.IsDeleted = true;
        _context.Brands.Update(brand);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}