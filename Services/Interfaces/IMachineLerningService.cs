using ServiceStationApp.Extensions.ViewModels;
using ServiceStationApp.Extensions.ViewModels.Model;

namespace ServiceStationApp.Services.Interfaces
{
    public interface IMachineLerningService
    {
        Task<ModelStats> GetStatistics();
        Task<PredictionResponse> Predict(ProblemDescription problem);
        Task<string> RetrainModel(RetrainRequest request);
    }
}