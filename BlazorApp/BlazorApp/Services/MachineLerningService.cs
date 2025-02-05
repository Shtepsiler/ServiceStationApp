using BlazorApp.Extensions.ViewModels.VehicleVMs;
using BlazorApp.Extensions;
using BlazorApp.Extensions.ViewModels.Model;
using BlazorApp.Services.Interfaces;
using BlazorApp.Extensions.ViewModels;

namespace BlazorApp.Services
{
    public class MachineLerningService : IMachineLerningService
    {
        private readonly ApiHttpClient httpClient;


        public MachineLerningService(IHttpClientFactory clientFactory, ApiHttpClient httpClient)
        {
            this.httpClient = httpClient.SetHttpClient(clientFactory.CreateClient("Model"));
        }

        public async Task<PredictionResponse> Predict(ProblemDescription problem)
        {
            var parameters = new Dictionary<string, string> { };
            return await httpClient.PostAsync<ProblemDescription, PredictionResponse>("predict", problem);
        }

        public async Task<string> RetrainModel(RetrainRequest request)
        {
            return await httpClient.PostAsyncGetString<RetrainRequest>("retrain", request);
        }

        public async Task<ModelStats> GetStatistics()
        {
            return await httpClient.GetAsync<ModelStats>("model-statistics");

        }


    }
}
