namespace BlazorApp.Extensions.ViewModels.JobsVMs
{
    public class JobVMForUser
    {
        public Guid? Id { get; set; }
        public string? ModelName { get; set; }
        public Guid VehicleId { get; set; }
        public string? Status { get; set; }
        public Guid? ClientId { get; set; }
        public DateTime? IssueDate { get; set; }
        public string? Description { get; set; }

    }
}
