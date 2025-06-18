using BlazorApp.Extensions;
using BlazorApp.Extensions.ViewModels.JobsVMs;
using BlazorApp.Services.Interfaces;

namespace BlazorApp.Services
{
    public class TasksService : ITasksService
    {
        private readonly ApiHttpClient httpClient;

        public TasksService(IHttpClientFactory clientFactory, ApiHttpClient httpClient)
        {

            this.httpClient = httpClient.SetHttpClient(clientFactory.CreateClient("Task"));

        }

        public async Task AddTask(TaskViewModel taskViewModel)
        {
            var parameters = new Dictionary<string, string> { };

            await httpClient.PostAsync("", parameters, taskViewModel);
        }

        public async Task DeleteTask(Guid Id)
        {
            await httpClient.DeleteAsync(Id.ToString());
        }

        public async Task<TaskViewModel> GetTask(Guid Id)
        {
            return await httpClient.GetAsync<TaskViewModel>(Id.ToString());
        }

        public async Task<IEnumerable<TaskViewModel>> GetTasks()
        {
            return await httpClient.GetAsync<IEnumerable<TaskViewModel>>("");
        }

        public async Task<IEnumerable<TaskViewModel>> GetTasksByJobId(Guid Id)
        {
            var parameters = new Dictionary<string, string> { { "Id", $"{Id.ToString()}" } };

            return await httpClient.GetAsync<IEnumerable<TaskViewModel>>("GetTasksByJobId", parameters);
        }

        public async Task<IEnumerable<TaskViewModel>> GetTasksByMechanic(Guid Id)
        {
            var parameters = new Dictionary<string, string> { { "Id", $"{Id.ToString()}" } };
            return await httpClient.GetAsync<IEnumerable<TaskViewModel>>("GetTasksByMechanicId", parameters);
        }

        public async Task UpdateTask(TaskViewModel taskViewModel)
        {
            await httpClient.PutAsync("", taskViewModel);
        }
        public async Task UpdateTaskStatus(Guid id, string status)
        {
            var parameters = new Dictionary<string, string> {
                { "Id", $"{id.ToString()}" } ,
                { "Status", status }
            };

            await httpClient.PutParametrsAsync("updateStatus", parameters);
        }
        public async Task<Pagination<TaskViewModel>> GetTasksPaginatedAsync(int pageNumber, int pageSize, string searchTerm = "", Guid? mechanicId = null, Status? status = null)
        {
            var parameters = new Dictionary<string, string>
            {
                { "pageNumber", pageNumber.ToString() },
                { "pageSize", pageSize.ToString() }
            };

            if (!string.IsNullOrEmpty(searchTerm))
                parameters.Add("searchTerm", searchTerm);

            if (mechanicId.HasValue)
                parameters.Add("mechanicId", mechanicId.Value.ToString());

            if (status.HasValue)
                parameters.Add("status", status.Value.ToString());

            return await httpClient.GetAsync<Pagination<TaskViewModel>>("GetTasksPaginated", parameters);
        }

    }
}
