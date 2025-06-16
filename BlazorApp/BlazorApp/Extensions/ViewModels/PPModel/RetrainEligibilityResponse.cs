namespace BlazorApp.Extensions.ViewModels.PPModel
{
    public class RetrainEligibilityResponse
    {
        public Dictionary<string, object>? config { get; set; }
        public bool eligible { get; set; }
        public int failed_count { get; set; }
        public int failed_retrains { get; set; }
        public string? last_retrain { get; set; }
        public double min_interval { get; set; }
        public TrainingProgress? progress { get; set; }
        public string? reason { get; set; }
        public string strategy { get; set; } = "";
        public double time_since_last { get; set; }
    }
}