using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Interfaces.Repositories;

public interface IBrandRepository
{
    Task<List<Brand>> GetAllAsync();

    Task<Brand?> GetByIdAsync(Guid id);

    Task AddAsync(Brand brand);

    void Update(Brand brand);

    void Delete(Brand brand);

    Task SaveChangesAsync();
}