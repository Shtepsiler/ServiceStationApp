using BlazorApp.Extensions;
using BlazorApp.Extensions.ViewModels.Model;
using BlazorApp.Extensions.ViewModels.VehicleVMs;
using BlazorApp.Services.Interfaces;

namespace BlazorApp.Services
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
