using BlazorApp.Components.Catalog;
using BlazorApp.Extensions;
using BlazorApp.Extensions.ViewModels.CatalogVMs;
using BlazorApp.Services.Interfaces;

namespace BlazorApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApiHttpClient httpClient;

        public OrderService(IHttpClientFactory clientFactory, ApiHttpClient httpClient)
        {

            this.httpClient = httpClient.SetHttpClient(clientFactory.CreateClient("Order"));

        }
        public async Task<OrderViewModel> GetNewOrder()
        {
            return await httpClient.GetAsync<OrderViewModel>("GetNewOrder");
        }

        public async Task<OrderViewModel> GetOrderByIdAsync(Guid orderId)
        {
            return await httpClient.GetAsync<OrderViewModel>(orderId.ToString());
        }
        public async Task RemovePartFromOrderAsync(Guid orderId, Guid partId)
        {
            var parameters = new Dictionary<string, string> {    
                {"orderId",orderId.ToString() },
                {"partId",partId.ToString() } };

            await httpClient.PostAsync("RemovePartFromOrder", parameters);
        }
        public async Task AddPartToOrderAsync(Guid orderId, Guid partId)
        {
            var parameters = new Dictionary<string, string> {
                {"orderId",orderId.ToString() },
                {"partId",partId.ToString() }
            };

            await httpClient.PostAsync("AddPartToOrder", parameters);
        }
    }
}
