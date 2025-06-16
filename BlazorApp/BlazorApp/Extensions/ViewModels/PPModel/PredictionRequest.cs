namespace BlazorApp.Extensions.ViewModels.PPModel;

public class PredictionRequest
{
    public double confidence_threshold { get; set; } = 0.1;
    public bool include_explanations { get; set; } = false;
    public string language { get; set; } = "uk";
    public string problem_description { get; set; } = "";
    public int top_k { get; set; } = 5;
    public string? user_id { get; set; }
}