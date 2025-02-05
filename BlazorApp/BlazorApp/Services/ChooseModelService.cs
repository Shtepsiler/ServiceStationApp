using BlazorApp.Extensions.ViewModels.CatalogVMs;
using BlazorApp.Extensions;
using BlazorApp.Extensions.ViewModels.JobsVMs;
using BlazorApp.Services.Interfaces;

namespace BlazorApp.Services
{
    public class ChooseModelService : IChooseModelService
    {
        private readonly ApiHttpClient httpPartsClient;

        public ChooseModelService(IHttpClientFactory clientFactory, ApiHttpClient httpClient)
        {
            this.httpPartsClient = httpClient.SetHttpClient(clientFactory.CreateClient("Parts"));
        }


        public async Task<IEnumerable<ChooseMakeResponse>> GetCarMakesAsync()
        {
            return await httpPartsClient.GetAsync<IEnumerable<ChooseMakeResponse>>("Make/titles");
        }

        public async Task<IEnumerable<ChooseModelResponse>> GetCarModelsByMakeAsync(Guid Id)
        {

            return await httpPartsClient.GetAsync<IEnumerable<ChooseModelResponse>>($"Model/titles?Id={Id.ToString()}");
        }

        public async Task<IEnumerable<ChooseSubModeResponse>> GetCarSubModelsByModelAsync(Guid Id)
        {

            return await httpPartsClient.GetAsync<IEnumerable<ChooseSubModeResponse>>($"SubModel/titles?Id={Id.ToString()}");
        }


    }
}
