
namespace BlazorApp.Extensions.ViewModels.CatalogVMs
{
    public class OrderViewModel
    {
        public Guid? Id { get; set; }
        public DateTime? Timestamp { get; set; }
        public int? OrderIndex { get; set; }
        public Guid? СustomerId { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? WEIPrice { get; set; }
        public bool IsPaid { get; set; }
        public OrderStatus Status { get; set; }
        public IEnumerable<PartViewModel> Parts { get; set; }
    }
    public enum OrderStatus
    {
        Pending,
        Paid
    }
}
