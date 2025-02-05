using ServiceStationApp.Extensions.ViewModels.CatalogVMs;

namespace ServiceStationApp.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderViewModel> GetOrderByIdAsync(Guid orderId);
        Task RemovePartFromOrderAsync(Guid orderId, Guid partId);
        Task AddPartToOrderAsync(Guid orderId, Guid partId);
        Task<OrderViewModel> GetNewOrder();
    }
}
