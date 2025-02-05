using BlazorApp.Extensions;
using BlazorApp.Extensions.ViewModels.CatalogVMs;
using BlazorApp.Services.Interfaces;

namespace BlazorApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApiHttpClient httpClient;

        public CategoryService(IHttpClientFactory clientFactory, ApiHttpClient httpClient)
        {

            this.httpClient = httpClient.SetHttpClient(clientFactory.CreateClient("Category"));

        }

        public async Task AddCategoryAsync(CategoryViewModel categoryViewModel)
        {
            var parameters = new Dictionary<string, string> { };

            await httpClient.PostAsync("", parameters, categoryViewModel);
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync()
        {
         return await httpClient.GetAsync<IEnumerable<CategoryViewModel>>("");
        }

        public async Task<CategoryViewModel> GetCategoryByIdAsync(Guid Id)
        {
            return await httpClient.GetAsync<CategoryViewModel>(Id.ToString());
        }

        public async Task UpdateCategoryAsync(CategoryViewModel categoryViewModel)
        {
            await httpClient.PutAsync("",categoryViewModel);    
        }
    }
}
