namespace BlazorApp.Extensions.ViewModels.CatalogVMs
{
    public class CategoryViewModel
    {
        public Guid? Id { get; set; }
        public DateTime? Timestamp { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public List<PartViewModel>? Parts { get; set; }
    }
}
