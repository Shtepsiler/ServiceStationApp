using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlazorApp.Extensions.ViewModels
{
    public class ModelStats
    {
        [JsonPropertyName("model_summary")]
        public string ModelSummary { get; set; }

        [JsonPropertyName("metrics")]
        public Metrics Metrics { get; set; }

        [JsonPropertyName("classification_report")]
        public ClassificationReport ClassificationReport { get; set; }

        [JsonPropertyName("confusion_matrix")]
        public List<ConfusionMatrixEntry> ConfusionMatrix { get; set; }
    }

    public class Metrics
    {
        [JsonPropertyName("accuracy")]
        public float Accuracy { get; set; }

        [JsonPropertyName("loss")]
        public float Loss { get; set; }
    }

    public class ClassificationReport
    {
        [JsonPropertyName("accuracy")]
        public float Accuracy { get; set; }

        [JsonPropertyName("macro_avg_precision")]
        public float MacroAvgPrecision { get; set; }

        [JsonPropertyName("macro_avg_recall")]
        public float MacroAvgRecall { get; set; }

        [JsonPropertyName("macro_avg_f1")]
        public float MacroAvgF1 { get; set; }

        [JsonPropertyName("weighted_avg_precision")]
        public float WeightedAvgPrecision { get; set; }

        [JsonPropertyName("weighted_avg_recall")]
        public float WeightedAvgRecall { get; set; }

        [JsonPropertyName("weighted_avg_f1")]
        public float WeightedAvgF1 { get; set; }

        [JsonPropertyName("class_metrics")]
        public Dictionary<string, ClassMetrics> ClassMetrics { get; set; }
    }

    public class ClassMetrics
    {
        [JsonPropertyName("precision")]
        public float Precision { get; set; }

        [JsonPropertyName("recall")]
        public float Recall { get; set; }

        [JsonPropertyName("f1_score")]
        public float F1Score { get; set; }

        [JsonPropertyName("support")]
        public float Support { get; set; }
    }

    public class ConfusionMatrixEntry
    {
        [JsonPropertyName("class_name")]
        public string ClassName { get; set; }

        [JsonPropertyName("predictions")]
        public List<int> Predictions { get; set; }
    }
}