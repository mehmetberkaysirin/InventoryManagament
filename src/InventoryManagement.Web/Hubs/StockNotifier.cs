using InventoryManagement.Application.Interfaces.Services;
using InventoryManagement.Web.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace InventoryManagement.Web.Services
{
    public class StockNotifier : IStockNotifier
    {
        private readonly IHubContext<InventoryHub> _hubContext;

        public StockNotifier(IHubContext<InventoryHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyStockUpdatedAsync()
        {
            await _hubContext.Clients.All.SendAsync("ReceiveStockUpdate");
        }
    }
}