using ServiceStationApp.Extensions.ViewModels.CatalogVMs;

namespace ServiceStationApp.Services.Interfaces
{
    public interface IPartsService
    {
        Task<IEnumerable<PartViewModel>> GetPartsByOrderIdAsync(Guid orderId);
        Task<PartViewModel> GetPartByIdAsync(Guid id);
        Task UpdatePartAsync(PartViewModel partViewModel);
        Task AddPartAsync(PartViewModel partViewModel);
        Task<IEnumerable<PartViewModel>> GetAllPartsAsync();
    }
}
