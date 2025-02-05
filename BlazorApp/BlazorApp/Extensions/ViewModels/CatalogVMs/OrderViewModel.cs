namespace BlazorApp.Extensions.ViewModels.CatalogVMs
{
    public class OrderViewModel
    {
        public Guid? Id { get; set; }
        public DateTime? Timestamp { get; set; }
        public Guid? СustomerId { get; set; }
        public IEnumerable<PartViewModel> Parts { get; set; }
    }
}
