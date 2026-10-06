using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Interfaces.Repositories;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync();

    Task<Stock?> GetByIdAsync(Guid id);

    Task AddAsync(Stock stock);

    void Update(Stock stock);

    void Delete(Stock stock);

    Task SaveChangesAsync();
    Task UpdateAsync(Stock stock); // <-- Bu imzanın olduğundan emin ol
    Task UpdateQuantityAsync(Guid stockId, decimal newQuantity);
}
