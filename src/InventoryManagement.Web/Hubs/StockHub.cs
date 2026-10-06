using Microsoft.AspNetCore.SignalR;

namespace InventoryManagement.Web.Hubs
{
    public class StockHub : Hub
    {
        // İstemciden sunucuya test mesajı
        public async Task SendTestMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveStockMovementUpdate");
        }
    }
}