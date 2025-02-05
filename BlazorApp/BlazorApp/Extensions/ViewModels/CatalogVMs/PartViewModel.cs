namespace BlazorApp.Extensions.ViewModels.CatalogVMs
{
    public class PartViewModel
    {
        public Guid? Id { get; set; }
        public DateTime? Timestamp { get; set; }
        public string? PartNumber { get; set; }
        public string? ManufacturerNumber { get; set; }
        public string? Description { get; set; }
        public string? PartName { get; set; }
        public bool? IsUniversal { get; set; }
        public int? PriceRegular { get; set; }
        public string? PartTitle { get; set; }
        public string? PartAttributes { get; set; }
        public Guid CategoryId { get; set; }

    }
}
