﻿namespace BlazorApp.Extensions.ViewModels.JobsVMs
{
    public class TaskViewModel
    {
        public Guid Id { get; set; }
        public Guid MechanicId { get; set; }

        public Guid? JobId { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }

        public string? Task { get; set; }
        public Status Status { get; set; }
    }
}
