using BlazorApp.Extensions.ViewModels.JobsVMs;

namespace BlazorApp.Services.Interfaces
{
    public interface IChooseModelService
    {
        Task<IEnumerable<ChooseMakeResponse>> GetCarMakesAsync();
        Task<IEnumerable<ChooseModelResponse>> GetCarModelsByMakeAsync(Guid Id);
        Task<IEnumerable<ChooseSubModeResponse>> GetCarSubModelsByModelAsync(Guid Id);
    }
}