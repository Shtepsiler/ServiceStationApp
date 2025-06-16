namespace BlazorApp.Extensions.ViewModels.PPModel
{
    public class BatchPredictionResponse
    {
        public string batch_id { get; set; } = "";
        public List<PredictionResponse> results { get; set; } = new();
        public DateTime timestamp { get; set; }
        public int total_processed { get; set; }
        public double total_processing_time { get; set; }
    }
}