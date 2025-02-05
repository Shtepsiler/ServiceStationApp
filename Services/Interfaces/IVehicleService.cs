using ServiceStationApp.Extensions.ViewModels.JobsVMs;
using ServiceStationApp.Extensions.ViewModels.VehicleVMs;

namespace ServiceStationApp.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleViewModel>> GetCarModelsAsync();
        Task<VehicleViewModel> CreateCarModelAsync(VehicleViewModel vehicleViewModel);
        Task<Guid> CreateVehicle(CreateVehicleViewModel vehicleViewModel);
        Task<VehicleViewModel> GetById(Guid Id);

    }
}
