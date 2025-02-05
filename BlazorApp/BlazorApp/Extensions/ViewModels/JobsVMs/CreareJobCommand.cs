using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Extensions.ViewModels.JobsVMs
{
    public class CreateJobCommand
    {
        public Guid? ClientId { get; set; }
        public Guid? VehicleId { get; set; }
        public Guid? MakeId { get; set; }
        public Guid? ModelId { get; set; }
        public Guid? SubModelId { get; set; }
        public int Year { get; set; }
        public string VIN { get; set; }
        [Required(ErrorMessage = "Введіть дату проблеми")]
        public DateTime IssueDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Введіть опис проблеми")]
        public string Description { get; set; }
    }
}
