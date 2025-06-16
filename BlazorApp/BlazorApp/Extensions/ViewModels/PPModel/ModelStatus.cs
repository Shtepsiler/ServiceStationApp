namespace BlazorApp.Extensions.ViewModels.PPModel
{
    public class ModelStatus
    {
        public double accuracy { get; set; }
        public int active_learning_queue { get; set; }
        public int? classes_count { get; set; }
        public int feedback_count { get; set; }
        public DateTime? last_retrain { get; set; }
        public string ml_model_version { get; set; } = "";
        public long? model_file_size { get; set; }
        public int total_predictions { get; set; }
        public int? training_samples { get; set; }
    }
}