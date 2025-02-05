using System.Text.Json.Serialization;

namespace BlazorApp.Extensions.ViewModels.Model
{
    public class RetrainRequest
    {
     
        public List<Guid> ids { get; set; } = new();
        public List<string> new_data { get; set; } = new();
        public List<string> new_labels { get; set; } = new();
    }
}
