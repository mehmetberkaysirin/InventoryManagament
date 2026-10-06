using Microsoft.AspNetCore.SignalR;

namespace InventoryManagement.Web.Hubs
{
    public class InventoryHub : Hub
    {
        // Şimdilik istemci bağlantılarını yönetmek ve sinyal fırlatmak için 
        // boş bir hub sınıfı yeterlidir. İleride özel grup veya kullanıcı bazlı 
        // bildirimler eklemek istersek burayı genişletebiliriz.
    }
}