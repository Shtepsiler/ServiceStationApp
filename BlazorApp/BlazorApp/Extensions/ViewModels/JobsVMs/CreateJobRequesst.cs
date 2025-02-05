using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Extensions.ViewModels.JobsVMs
{
    public class CreateJobRequesst
    {
        public Guid? ClientId { get; set; }
        public Guid? VehicleId { get; set; }

        [Required(ErrorMessage = "Введіть дату проблеми")]
        public DateTime IssueDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Введіть опис проблеми")]
        public string Description { get; set; }
    }
}
