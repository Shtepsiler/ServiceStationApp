using BlazorApp.Extensions;
using BlazorApp.Extensions.ViewModels.VehicleVMs;
using BlazorApp.Services.Interfaces;

namespace BlazorApp.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly ApiHttpClient httpClient;


        public VehicleService(IHttpClientFactory clientFactory, ApiHttpClient httpClient)
        {
            this.httpClient = httpClient.SetHttpClient(clientFactory.CreateClient("Vehicle"));
        }

        public async Task<VehicleViewModel> CreateCarModelAsync(VehicleViewModel vehicleViewModel)
        {
            var parameters = new Dictionary<string, string> { };
            return await httpClient.PostAsync<VehicleViewModel, VehicleViewModel>("", vehicleViewModel);
        }

        public async Task<IEnumerable<VehicleViewModel>> GetCarModelsAsync()
        {
            return await httpClient.GetAsync<IEnumerable<VehicleViewModel>>("");
        }
        public async Task<Guid> CreateVehicle(CreateVehicleViewModel vehicleViewModel)
        {
            return Guid.Parse(await httpClient.PostAsyncGetString<CreateVehicleViewModel >("new", vehicleViewModel));
        }

        public async Task<VehicleViewModel> GetById(Guid Id)
        {
            return await httpClient.GetAsync<VehicleViewModel>(Id.ToString());

        }




    }
}
