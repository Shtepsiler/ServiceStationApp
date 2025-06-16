namespace BlazorApp.Extensions.ViewModels.PPModel
{
    public class RetrainTriggerResponse
    {
        public double baseline_performance { get; set; }
        public string? error { get; set; }
        public string? message { get; set; }
        public TrainingProgress? progress { get; set; }
        public bool success { get; set; }
    }
}