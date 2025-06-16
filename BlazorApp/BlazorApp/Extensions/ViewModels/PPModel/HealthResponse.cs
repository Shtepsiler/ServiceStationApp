namespace BlazorApp.Extensions.ViewModels.PPModel
{
    public class HealthResponse
    {
        public Dictionary<string, object>? environment { get; set; }
        public bool? gpu_available { get; set; }
        public Dictionary<string, double>? memory_usage { get; set; }
        public bool ml_model_loaded { get; set; }
        public bool redis_connected { get; set; }
        public string status { get; set; } = "";
        public double? uptime_seconds { get; set; }
        public string version { get; set; } = "";
    }
}