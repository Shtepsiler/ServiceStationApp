using ServiceStationApp.Extensions.ViewModels.VehicleVMs;
using ServiceStationApp.Extensions;
using ServiceStationApp.Extensions.ViewModels.Model;
using ServiceStationApp.Services.Interfaces;

namespace ServiceStationApp.Services
{
    public class SpecialisationsService : ISpecialisationsService
    {
        private readonly ApiHttpClient httpClient;

        public SpecialisationsService(IHttpClientFactory clientFactory, ApiHttpClient httpClient)
        {

            this.httpClient = httpClient.SetHttpClient(clientFactory.CreateClient("Specialisations"));

        }
        public async Task<IEnumerable<SpecialisationVM>> GetSpecialisationsAsync()
        {
            return await httpClient.GetAsync<IEnumerable<SpecialisationVM>>("brief");
        }
    }
}
