namespace BlazorApp.Extensions.ViewModels
{
    public class ProductViewModel
    {
        public Guid? Id { get; set; } // id товару

        public string? Name { get; set; } = ""; // Назва товару
        public string? Category { get; set; } = ""; // Категорія товару
        public string? Price { get; set; } = ""; // ціна товару

    }
}
