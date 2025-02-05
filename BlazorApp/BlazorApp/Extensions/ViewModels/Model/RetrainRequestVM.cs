namespace BlazorApp.Extensions.ViewModels.Model
{
    public class RetrainRequestVM
    {
        public RetrainRequestVM(Guid id, string new_data, string new_label)
        {
            this.id = id;
            this.new_data = new_data;
            this.new_label = new_label;
        }

        public Guid id { get; set; }
        public string new_data { get; set; }
        public string new_label { get; set; }
    }
}
