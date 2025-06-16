using BlazorApp.Extensions;
using BlazorApp.Extensions.ViewModels.PPModel;
using BlazorApp.Services.Interfaces;

namespace BlazorApp.Services
{
    public class PartsMLService : IPartsMLService
    {
        private readonly ApiHttpClient httpClient;

        public PartsMLService(IHttpClientFactory clientFactory, ApiHttpClient httpClient)
        {
            this.httpClient = httpClient.SetHttpClient(clientFactory.CreateClient("PPModel"));
        }

        // Core prediction functionality
        public async Task<PredictionResponse> Predict(PredictionRequest request)
        {
            return await httpClient.PostAsync<PredictionRequest, PredictionResponse>("predict", request);
        }

        public async Task<BatchPredictionResponse> BatchPredict(BatchPredictionRequest request)
        {
            return await httpClient.PostAsync<BatchPredictionRequest, BatchPredictionResponse>("predict/batch", request);
        }

        // Feedback and active learning
        public async Task<FeedbackResponse> SubmitFeedback(FeedbackRequest feedback)
        {
            return await httpClient.PostAsync<FeedbackRequest, FeedbackResponse>("feedback", feedback);
        }

        public async Task<BatchFeedbackResponse> SubmitBatchFeedback(List<FeedbackRequest> feedbacks)
        {
            return await httpClient.PostAsync<List<FeedbackRequest>, BatchFeedbackResponse>("feedback/batch", feedbacks);
        }

        public async Task<UncertainSamplesResponse> GetUncertainSamples(int limit = 10)
        {
            var parameters = new Dictionary<string, string> { ["limit"] = limit.ToString() };
            return await httpClient.GetAsync<UncertainSamplesResponse>("uncertain-samples", parameters);
        }

        // Status and monitoring
        public async Task<RootResponse> GetRoot()
        {
            return await httpClient.GetAsync<RootResponse>("");
        }

        public async Task<HealthResponse> GetHealth()
        {
            return await httpClient.GetAsync<HealthResponse>("health");
        }

        public async Task<ModelStatus> GetStatus()
        {
            return await httpClient.GetAsync<ModelStatus>("status");
        }

        public async Task<MetricsResponse> GetMetrics()
        {
            return await httpClient.GetAsync<MetricsResponse>("metrics");
        }

        // Admin - Learning statistics
        public async Task<LearningStatisticsResponse> GetLearningStatistics()
        {
            return await httpClient.GetAsync<LearningStatisticsResponse>("admin/learning/statistics");
        }

        // Admin - Retraining management
        public async Task<RetrainStatusResponse> GetRetrainStatus()
        {
            return await httpClient.GetAsync<RetrainStatusResponse>("admin/retrain/status");
        }

        public async Task<RetrainTriggerResponse> TriggerRetrain()
        {
            return await httpClient.PostAsync<object, RetrainTriggerResponse>("admin/retrain/trigger", new { });
        }

        public async Task<TrainingProgress> GetTrainingProgress()
        {
            return await httpClient.GetAsync<TrainingProgress>("admin/retrain/progress");
        }

        // Admin operations
        public async Task<CacheClearResponse> ClearCache()
        {
            return await httpClient.PostAsync<object, CacheClearResponse>("admin/clear-cache", new { });
        }

        // Helper methods for common use cases
        public async Task<bool> IsHealthy()
        {
            try
            {
                var health = await GetHealth();
                return health.status == "healthy";
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsModelLoaded()
        {
            try
            {
                var health = await GetHealth();
                return health.ml_model_loaded;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsTrainingInProgress()
        {
            try
            {
                var progress = await GetTrainingProgress();
                return progress.status == "training" || progress.status == "preparing_data" ||
                       progress.status == "validating" || progress.status == "deploying";
            }
            catch
            {
                return false;
            }
        }

        // Quick prediction with defaults
        public async Task<PredictionResponse> QuickPredict(string problemDescription, string? userId = null)
        {
            var request = new PredictionRequest
            {
                problem_description = problemDescription,
                user_id = userId,
                language = "uk",
                top_k = 5,
                confidence_threshold = 0.1
            };

            return await Predict(request);
        }

        // Quick feedback submission
        public async Task<FeedbackResponse> QuickFeedback(string predictionId, List<string> correctParts,
            bool isCorrect, int? rating = null, string? userId = null)
        {
            var feedback = new FeedbackRequest
            {
                prediction_id = predictionId,
                correct_parts = correctParts,
                is_correct_prediction = isCorrect,
                user_rating = rating,
                user_id = userId
            };

            return await SubmitFeedback(feedback);
        }
    }
}