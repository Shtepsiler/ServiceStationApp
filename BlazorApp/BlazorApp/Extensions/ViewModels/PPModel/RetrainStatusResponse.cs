namespace BlazorApp.Extensions.ViewModels.PPModel
{
    public class RetrainStatusResponse
    {
        public LearningStatistics learning_statistics { get; set; } = new();
        public RetrainEligibilityResponse retrain_eligibility { get; set; } = new();
        public string timestamp { get; set; } = "";
    }
}