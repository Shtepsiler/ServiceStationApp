namespace BlazorApp.Extensions.ViewModels.PPModel;

public class CurrentCounts
{
    public int feedback_received { get; set; }
    public int predictions_stored { get; set; }
    public int uncertainty_queue_size { get; set; }
}