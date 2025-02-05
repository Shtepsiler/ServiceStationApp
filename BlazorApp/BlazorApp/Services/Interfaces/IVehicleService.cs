using BlazorApp.Extensions.ViewModels.JobsVMs;
using BlazorApp.Extensions.ViewModels.VehicleVMs;

namespace BlazorApp.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleViewModel>> GetCarModelsAsync();
        Task<VehicleViewModel> CreateCarModelAsync(VehicleViewModel vehicleViewModel);
        Task<Guid> CreateVehicle(CreateVehicleViewModel vehicleViewModel);
        Task<VehicleViewModel> GetById(Guid Id);

    }
}
