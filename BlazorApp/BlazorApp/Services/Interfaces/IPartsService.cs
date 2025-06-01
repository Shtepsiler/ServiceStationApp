using BlazorApp.Extensions.ViewModels.CatalogVMs;

namespace BlazorApp.Services.Interfaces
{
    public interface IPartsService
    {
        Task<IEnumerable<PartViewModel>> GetPartsByOrderIdAsync(Guid orderId);
        Task<PartViewModel> GetPartByIdAsync(Guid id);
        Task UpdatePartAsync(PartViewModel partViewModel);
        Task AddPartAsync(PartViewModel partViewModel);
        Task<IEnumerable<PartViewModel>> GetAllPartsAsync();
        Task<Pagination<PartViewModel>> GetAllPartsPaginatedAsync(
            int pageNumber,
            int pageSize = 10,
            string searchTerm = null,
            Guid? categoryId = null);
    }
}
