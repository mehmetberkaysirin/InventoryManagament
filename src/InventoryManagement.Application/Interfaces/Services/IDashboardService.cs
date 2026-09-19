using InventoryManagement.Application.DTOs.Dashboard;

namespace InventoryManagement.Application.Interfaces.Services
{
    public interface IDashboardService
    {
        Task<DashboardSummaryDto> GetDashboardSummaryAsync();
    }
}