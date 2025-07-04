﻿namespace BlazorApp.Extensions.ViewModels.JobsVMs
{
    public class JobVM
    {
        public Guid? Id { get; set; }
        public Guid? ManagerId { get; set; }
        public Guid? VehicleId { get; set; }
        public string? ModelName { get; set; }
        public Status? Status { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? MechanicId { get; set; }
        public Guid? OrderId { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public bool IsPaid { get; set; }
        public int? jobIndex { get; set; }
        public string? Specialisation { get; set; }
        public string? WEIPrice { get; set; }
        public string? PredictionId { get; set; }
        public List<TaskViewModel> Tasks { get; set; } = new();


    }
}
