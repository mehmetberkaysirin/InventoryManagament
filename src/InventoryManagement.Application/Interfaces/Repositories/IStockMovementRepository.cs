using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Interfaces.Repositories;

public interface IStockMovementRepository
{
    Task<List<StockMovement>> GetAllAsync();

    Task AddAsync(StockMovement movement);

    Task SaveChangesAsync();
}