using ServiceStationApp.Extensions.ViewModels.JobsVMs;

namespace ServiceStationApp.Services.Interfaces
{
    public interface IMechanicsService
    {
        Task CreateMechanicAsync(MechanicVM command);
        Task<IEnumerable<MechanicVM>> GetMechanicsAsync();
    }
}