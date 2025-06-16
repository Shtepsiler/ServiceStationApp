namespace BlazorApp.Extensions.ViewModels.PPModel
{
    public class BatchPredictionRequest
    {
        public bool include_individual_ids { get; set; } = true;
        public List<string> problems { get; set; } = new();
        public string? user_id { get; set; }
    }
}