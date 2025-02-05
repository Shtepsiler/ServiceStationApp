using ServiceStationApp.Extensions.ViewModels.Model;

namespace ServiceStationApp.Services.Interfaces
{
    public interface ISpecialisationsService
    {
        Task<IEnumerable<SpecialisationVM>> GetSpecialisationsAsync();

    }
}
