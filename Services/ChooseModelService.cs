using ServiceStationApp.Extensions.ViewModels.CatalogVMs;
using ServiceStationApp.Services.Interfaces;
using ServiceStationApp.Extensions.ViewModels.JobsVMs;
using ServiceStationApp.Extensions;

namespace ServiceStationApp.Services
{
    public class ChooseModelService : IChooseModelService
    {
        private readonly ApiHttpClient httpPartsClient;

        public ChooseModelService(IHttpClientFactory clientFactory, ApiHttpClient httpClient)
        {
            httpPartsClient = httpClient.SetHttpClient(clientFactory.CreateClient("Parts"));
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
