using BlazorApp.Extensions;
using BlazorApp.Extensions.ViewModels.CatalogVMs;
using BlazorApp.Services.Interfaces;

namespace BlazorApp.Services
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

        public async Task<Pagination<PartViewModel>> GetAllPartsPaginatedAsync(
            int pageNumber,
            int pageSize = 10,
            string searchTerm = null,
            Guid? categoryId = null)
        {
            var parameters = new Dictionary<string, string>
            {
                ["pageNumber"] = pageNumber.ToString(),
                ["pageSize"] = pageSize.ToString()
            };

            if (!string.IsNullOrEmpty(searchTerm))
                parameters["search"] = searchTerm;

            if (categoryId.HasValue)
                parameters["categoryId"] = categoryId.Value.ToString();

            return await httpClient.GetAsync<Pagination<PartViewModel>>("paginated", parameters);
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
