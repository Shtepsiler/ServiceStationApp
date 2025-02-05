namespace BlazorApp.Extensions.ViewModels.Model
{
    public class PredictionResponse
    {
        public string predicted_class { get; set; }
        public float confidence { get; set; }
    }
}
