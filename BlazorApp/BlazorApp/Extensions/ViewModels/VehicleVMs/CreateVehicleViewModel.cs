namespace BlazorApp.Extensions.ViewModels.VehicleVMs
{
    public class CreateVehicleViewModel
    {
        public Guid? MakeId { get; set; }
        public Guid? ModelId { get; set; }
        public Guid? SubModelId { get; set; }
        public int Year { get; set; }
        public string? VIN { get; set; }

    }
}
