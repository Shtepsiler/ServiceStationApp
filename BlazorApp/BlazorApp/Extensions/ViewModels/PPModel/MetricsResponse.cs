namespace BlazorApp.Extensions.ViewModels.PPModel
{
    public class MetricsResponse
    {
        public bool active_learning_available { get; set; }
        public double cache_hit_rate { get; set; }
        public int cache_hits { get; set; }
        public int cache_misses { get; set; }
        public double feedback_ratio { get; set; }
        public int feedback_total { get; set; }
        public double model_accuracy { get; set; }
        public bool model_loaded { get; set; }
        public int predictions_total { get; set; }
        public bool redis_available { get; set; }
        public string timestamp { get; set; } = "";
        public double uptime_seconds { get; set; }
    }
}