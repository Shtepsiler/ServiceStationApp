namespace BlazorApp.Extensions.ViewModels.PPModel
{
    public class ModelInfo
    {
        public double accuracy { get; set; }
        public string device { get; set; } = "";
        public bool jit_enabled { get; set; }
        public bool model_loaded { get; set; }
        public string model_path { get; set; } = "";
        public string model_version { get; set; } = "";
        public int num_classes { get; set; }
        public int vocab_size { get; set; }
    }
}