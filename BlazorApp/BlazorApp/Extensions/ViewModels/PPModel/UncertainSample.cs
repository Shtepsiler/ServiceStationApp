namespace BlazorApp.Extensions.ViewModels.PPModel;

public class UncertainSample
{
    public double confidence { get; set; }
    public string prediction_id { get; set; } = "";
    public List<SimplePrediction> predictions { get; set; } = new();
    public string problem_description { get; set; } = "";
    public string timestamp { get; set; } = "";
}