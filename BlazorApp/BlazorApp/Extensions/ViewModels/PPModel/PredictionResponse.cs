namespace BlazorApp.Extensions.ViewModels.PPModel
{
    public class PredictionResponse
    {
        public bool cached { get; set; }
        public double confidence_score { get; set; }
        public string? language_detected { get; set; }
        public string ml_model_version { get; set; } = "";
        public string prediction_id { get; set; } = "";
        public List<PredictionItem> predictions { get; set; } = new();
        public double processing_time { get; set; }
        public List<Dictionary<string, double>> simple_predictions { get; set; } = new();
        public DateTime timestamp { get; set; }
    }
}