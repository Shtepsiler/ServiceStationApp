using BlazorApp.Extensions.ViewModels.Model;

namespace BlazorApp.Services.Interfaces
{
    public interface ISpecialisationsService
    {
        Task<IEnumerable<SpecialisationVM>> GetSpecialisationsAsync();

    }
}
