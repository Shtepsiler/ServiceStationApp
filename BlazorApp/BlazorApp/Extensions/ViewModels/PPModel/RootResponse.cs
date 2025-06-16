namespace BlazorApp.Extensions.ViewModels.PPModel
{
    public class RootResponse
    {
        public string docs { get; set; } = "";
        public string health { get; set; } = "";
        public string message { get; set; } = "";
        public RootStatus status { get; set; } = new();
        public string version { get; set; } = "";
    }
}