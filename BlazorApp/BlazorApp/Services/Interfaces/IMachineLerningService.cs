using BlazorApp.Extensions.ViewModels;
using BlazorApp.Extensions.ViewModels.Model;

namespace BlazorApp.Services.Interfaces
{
    public interface IMachineLerningService
    {
        Task<ModelStats> GetStatistics();
        Task<PredictionResponse> Predict(ProblemDescription problem);
        Task<string> RetrainModel(RetrainRequest request);
    }
}