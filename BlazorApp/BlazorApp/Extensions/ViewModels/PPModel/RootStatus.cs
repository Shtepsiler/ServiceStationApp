namespace BlazorApp.Extensions.ViewModels.PPModel;

public class RootStatus
{
    public bool model_loaded { get; set; }
    public bool redis_available { get; set; }
    public bool active_learning_available { get; set; }
    public double cache_hit_rate { get; set; }
}