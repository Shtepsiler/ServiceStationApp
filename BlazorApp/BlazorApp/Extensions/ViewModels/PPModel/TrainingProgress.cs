namespace BlazorApp.Extensions.ViewModels.PPModel
{
    public class TrainingProgress
    {
        public double baseline_performance { get; set; }
        public int data_size { get; set; }
        public string? end_time { get; set; }
        public int epoch_current { get; set; }
        public int epochs_total { get; set; }
        public string? error { get; set; }
        public string? final_error { get; set; }
        public Dictionary<string, object>? final_result { get; set; }
        public string start_time { get; set; } = "";
        public string status { get; set; } = "";
        public string strategy { get; set; } = "";
        public double train_loss { get; set; }
        public double training_time { get; set; }
        public double val_accuracy { get; set; }
    }
}