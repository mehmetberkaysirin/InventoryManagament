using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Interfaces.Repositories;

public interface IWarehouseRepository
{
    Task<List<Warehouse>> GetAllAsync();

    Task<Warehouse?> GetByIdAsync(Guid id);

    Task AddAsync(Warehouse warehouse);

    void Update(Warehouse warehouse);

    void Delete(Warehouse warehouse);

    Task SaveChangesAsync();
}