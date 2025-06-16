namespace BlazorApp.Extensions.ViewModels.PPModel
{
    public class PredictionItem
    {
        public string? category { get; set; }
        public double confidence { get; set; }
        public double? estimated_cost { get; set; }
        public string? explanation { get; set; }
        public string part_name { get; set; } = "";
    }
}

