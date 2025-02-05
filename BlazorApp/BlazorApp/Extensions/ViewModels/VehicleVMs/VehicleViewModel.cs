namespace BlazorApp.Extensions.ViewModels.VehicleVMs
{
    public class VehicleViewModel
    {
        public Guid? id { get; set; }
        public Guid? MakeId { get; set; }
        public Guid? ModelId { get; set; }
        public Guid? SubModelId { get; set; }
        public DateTime year { get; set; }
        public string? fullModelName { get; set; }
        public string? vin { get; set; }

    }
}
