namespace BlazorApp.Extensions.ViewModels.PPModel
{
    public class UncertainSamplesResponse
    {
        public int count { get; set; }
        public string? message { get; set; }
        public List<UncertainSample> samples { get; set; } = new();
    }
}