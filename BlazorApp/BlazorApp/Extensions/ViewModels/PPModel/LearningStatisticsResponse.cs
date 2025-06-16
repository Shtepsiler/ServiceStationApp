namespace BlazorApp.Extensions.ViewModels.PPModel
{
    public class LearningStatisticsResponse
    {
        public LearningStatistics active_learning { get; set; } = new();
        public ModelInfo model_info { get; set; } = new();
        public string timestamp { get; set; } = "";
    }
}