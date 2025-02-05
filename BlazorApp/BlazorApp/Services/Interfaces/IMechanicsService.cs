using BlazorApp.Extensions.ViewModels.JobsVMs;

namespace BlazorApp.Services.Interfaces
{
    public interface IMechanicsService
    {
        Task CreateMechanicAsync(MechanicVM command);
        Task<IEnumerable<MechanicVM>> GetMechanicsAsync();
    }
}