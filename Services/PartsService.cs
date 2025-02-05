using ServiceStationApp.Extensions;
using ServiceStationApp.Extensions.ViewModels.CatalogVMs;
using ServiceStationApp.Services.Interfaces;

namespace ServiceStationApp.Services
{
    public class PartsService : IPartsService
    {
        private readonly ApiHttpClient httpClient;

        public PartsService(IHttpClientFactory clientFactory, ApiHttpClient httpClient)
        {

            this.httpClient = httpClient.SetHttpClient(clientFactory.CreateClient("Part"));

        }

        public async Task AddPartAsync(PartViewModel partViewModel)
        {
            var parameters = new Dictionary<string, string> { };

            await httpClient.PostAsync("", parameters, partViewModel);

        }

        public async Task<IEnumerable<PartViewModel>> GetAllPartsAsync()
        {
            return await httpClient.GetAsync<IEnumerable<PartViewModel>>("");
        }

        public async Task<PartViewModel> GetPartByIdAsync(Guid id)
        {
            return await httpClient.GetAsync<PartViewModel>(id.ToString());
        }



        public async Task UpdatePartAsync(PartViewModel partViewModel)
        {
            await httpClient.PutAsync("", partViewModel);
        }

        public async Task<IEnumerable<PartViewModel>> GetPartsByOrderIdAsync(Guid orderId)
        {
            return await httpClient.GetAsync<IEnumerable<PartViewModel>>($"GetPartsByOrderId?OrderId={orderId.ToString()}");
        }
    }
}
