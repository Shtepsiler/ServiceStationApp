namespace BlazorApp.Extensions.ViewModels.PPModel
{
    public class CacheClearResponse
    {
        public Dictionary<string, bool> cleared { get; set; } = new();
        public string message { get; set; } = "";
    }
}