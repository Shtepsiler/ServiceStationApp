using ServiceStationApp.Extensions.ViewModels.JobsVMs;
using System.Collections.Generic;

namespace ServiceStationApp.Services.Interfaces
{
    public interface ITasksService
    {
        Task DeleteTask(Guid Id);
        Task<IEnumerable<TaskViewModel>> GetTasksByMechanic(Guid Id);
        Task<IEnumerable<TaskViewModel>> GetTasks();
        Task<TaskViewModel> GetTask(Guid Id);
        Task AddTask(TaskViewModel taskViewModel);
        Task UpdateTask(TaskViewModel taskViewModel);
        Task<IEnumerable<TaskViewModel>> GetTasksByJobId(Guid Id);
        Task UpdateTaskStatus(Guid id, string status);
    }
}
