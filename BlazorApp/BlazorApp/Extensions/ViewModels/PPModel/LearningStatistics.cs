namespace BlazorApp.Extensions.ViewModels.PPModel;

public class LearningStatistics
{
    public Dictionary<string, object> basic_stats { get; set; } = new();
    public CurrentCounts current_counts { get; set; } = new();
    public PerformanceMetrics performance_metrics { get; set; } = new();
    public Dictionary<string, object> retrain_info { get; set; } = new();
    public string timestamp { get; set; } = "";
}