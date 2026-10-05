namespace InventoryManagement.Application.Interfaces.Services
{
    public interface IStockNotifier
    {
        Task NotifyStockUpdatedAsync();
    }
}