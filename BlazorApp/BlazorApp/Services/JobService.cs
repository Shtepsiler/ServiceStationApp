using BlazorApp.Extensions;
using BlazorApp.Extensions.ViewModels.JobsVMs;
using BlazorApp.Extensions.ViewModels.Model;
using BlazorApp.Services.Interfaces;

namespace BlazorApp.Services
{
    public class JobService : IJobService
    {
        private readonly ApiHttpClient httpClient;

        public JobService(IHttpClientFactory clientFactory, ApiHttpClient httpClient)
        {

            this.httpClient = httpClient.SetHttpClient(clientFactory.CreateClient("Job"));

        }


        public async Task UpdateModelAproved(UpdateModelAprovedMV request)
        {

            await httpClient.PutAsync<UpdateModelAprovedMV>("updateModelAproved", request);

        }



        public async Task CreateVisitAsync(CreateJobRequesst command)
        {
            var parameters = new Dictionary<string, string> { };

            await httpClient.PostAsync("", parameters, command);
        }

        public async Task AddOrder(Guid id,Guid OrderId)
        {
            var parameters = new Dictionary<string, string> {
                {"Id",id.ToString() },
                {"OrderId",OrderId.ToString() }
            };

            await httpClient.PostAsync("AddOrderToJob", parameters);
        }



        public async Task<IEnumerable<JobVM>> GetAllJobs()
        {
            return await httpClient.GetAsync<IEnumerable<JobVM>>("");
        }

        public async Task<JobVM> GetJobById(Guid Id)
        {
            return await httpClient.GetAsync<JobVM>(Id.ToString());
        }
        public async Task<RetrainRequest> GetRetrainData(float conf, bool choseApproverd)
        {
            // Форматуємо conf з використанням крапки як роздільника дробу
            var parameters = new Dictionary<string, string>
            {
                { "confidence", conf.ToString("G", System.Globalization.CultureInfo.InvariantCulture) },
                { "choseApproverd", $"{choseApproverd}" }
            };

            return await httpClient.GetAsync<RetrainRequest>("uncertain-samples", parameters);
        }


        public async Task UpdateStatus(Guid Id, string Status)
        {
            var parameters = new Dictionary<string, string>
            {
                { "Id",Id.ToString() },
                { "Status", Status }
            };
             await httpClient.PutParametrsAsync("updateStatus", parameters);
        }
        public async Task UpdateMecahin(Guid Id, Guid MechanicId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "Id",Id.ToString() },
                { "MechanicId", MechanicId.ToString()}
            };
            await httpClient.PutParametrsAsync("updateMechanic", parameters);
        }

        public async Task<IEnumerable<JobVM>> GetJobByMechanicId(Guid MechanicId)
        {
            var parameters = new Dictionary<string, string> { { "Id", $"{MechanicId.ToString()}" } };

            return await httpClient.GetAsync<IEnumerable<JobVM>>("GetJobByMechanicId", parameters);
        }



        public async Task<IEnumerable<JobVMForUser>> GetJobsBYUserId(Guid UserId)
        {
            var parameters = new Dictionary<string, string> { { "Id", $"{UserId.ToString()}" } };

            return await httpClient.GetAsync<IEnumerable<JobVMForUser>>("GetJobsBYUserId", parameters);
        }
    }
}
