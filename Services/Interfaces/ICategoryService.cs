using ServiceStationApp.Extensions.ViewModels.CatalogVMs;

namespace ServiceStationApp.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryViewModel> GetCategoryByIdAsync(Guid Id);
        Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync();

        Task AddCategoryAsync(CategoryViewModel categoryViewModel);
        Task UpdateCategoryAsync(CategoryViewModel categoryViewModel);


    }
}
