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

public class UnitRepository : IUnitRepository
{
    private readonly ApplicationDbContext _context;

    public UnitRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Unit>> GetAllAsync()
    {
        return await _context.Units
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .ToListAsync();
    }

    public async Task<Unit?> GetByIdAsync(Guid id)
    {
        return await _context.Units
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }

    public async Task AddAsync(Unit unit)
    {
        await _context.Units.AddAsync(unit);
    }

    public void Update(Unit unit)
    {
        _context.Units.Update(unit);
    }

    public void Delete(Unit unit)
    {
        unit.IsDeleted = true;
        _context.Units.Update(unit);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}