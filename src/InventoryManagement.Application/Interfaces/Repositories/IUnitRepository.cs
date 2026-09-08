using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Interfaces.Repositories;

public interface IUnitRepository
{
    Task<List<Unit>> GetAllAsync();

    Task<Unit?> GetByIdAsync(Guid id);

    Task AddAsync(Unit unit);

    void Update(Unit unit);

    void Delete(Unit unit);

    Task SaveChangesAsync();
}