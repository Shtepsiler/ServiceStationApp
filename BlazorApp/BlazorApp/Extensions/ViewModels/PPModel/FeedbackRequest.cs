namespace BlazorApp.Extensions.ViewModels.PPModel
{
    public class FeedbackRequest
    {
        public string? comments { get; set; }
        public List<string> correct_parts { get; set; } = new();
        public bool is_correct_prediction { get; set; }
        public string prediction_id { get; set; } = "";
        public double? time_to_feedback { get; set; }
        public string? user_id { get; set; }
        public int? user_rating { get; set; }
    }
}