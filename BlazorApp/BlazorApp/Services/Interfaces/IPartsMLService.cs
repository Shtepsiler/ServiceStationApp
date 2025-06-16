using BlazorApp.Extensions.ViewModels.PPModel;

namespace BlazorApp.Services.Interfaces;

public interface IPartsMLService
{
    Task<PredictionResponse> Predict(PredictionRequest request);
    Task<BatchPredictionResponse> BatchPredict(BatchPredictionRequest request);
    Task<FeedbackResponse> SubmitFeedback(FeedbackRequest feedback);
    Task<BatchFeedbackResponse> SubmitBatchFeedback(List<FeedbackRequest> feedbacks);
    Task<UncertainSamplesResponse> GetUncertainSamples(int limit = 10);
    Task<RootResponse> GetRoot();
    Task<HealthResponse> GetHealth();
    Task<ModelStatus> GetStatus();
    Task<MetricsResponse> GetMetrics();
    Task<LearningStatisticsResponse> GetLearningStatistics();
    Task<RetrainStatusResponse> GetRetrainStatus();
    Task<RetrainTriggerResponse> TriggerRetrain();
    Task<TrainingProgress> GetTrainingProgress();
    Task<CacheClearResponse> ClearCache();
    Task<bool> IsHealthy();
    Task<bool> IsModelLoaded();
    Task<bool> IsTrainingInProgress();
    Task<PredictionResponse> QuickPredict(string problemDescription, string? userId = null);

    Task<FeedbackResponse> QuickFeedback(string predictionId, List<string> correctParts,
        bool isCorrect, int? rating = null, string? userId = null);
}