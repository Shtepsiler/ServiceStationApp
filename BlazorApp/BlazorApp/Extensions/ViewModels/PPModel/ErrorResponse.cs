namespace BlazorApp.Extensions.ViewModels.PPModel
{
    public class ErrorResponse
    {
        public Dictionary<string, object>? details { get; set; }
        public string error { get; set; } = "";
        public string? error_code { get; set; }
        public string? request_id { get; set; }
        public DateTime timestamp { get; set; }
    }
}