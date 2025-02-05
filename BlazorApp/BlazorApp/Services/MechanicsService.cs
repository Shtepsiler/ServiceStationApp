using BlazorApp.Extensions.ViewModels.JobsVMs;
using BlazorApp.Extensions;
using BlazorApp.Services.Interfaces;

namespace BlazorApp.Services
{
    public class MechanicsService : IMechanicsService
    {
        private readonly ApiHttpClient httpClient;

        public MechanicsService(IHttpClientFactory clientFactory, ApiHttpClient httpClient)
        {

            this.httpClient = httpClient.SetHttpClient(clientFactory.CreateClient("Mecahics"));

        }



        public async Task<IEnumerable<MechanicVM>> GetMechanicsAsync()
        {

           return await httpClient.GetAsync<IEnumerable<MechanicVM>>("");

        }



        public async Task CreateMechanicAsync(MechanicVM command)
        {
            var parameters = new Dictionary<string, string> { };

            await httpClient.PostAsync("", parameters, command);
        }
    }
}
