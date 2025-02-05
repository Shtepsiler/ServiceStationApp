using BlazorApp.Extensions.ViewModels.JobsVMs;
using BlazorApp.Extensions.ViewModels.Model;

namespace BlazorApp.Services.Interfaces
{
    public interface IJobService
    {
        Task CreateVisitAsync(CreateJobRequesst command);
        Task<JobVM> GetJobById(Guid Id);
        Task<IEnumerable<JobVMForUser>> GetJobsBYUserId(Guid UserId);
        Task<IEnumerable<JobVM>> GetAllJobs();
        Task<IEnumerable<JobVM>> GetJobByMechanicId(Guid UserId);
        Task<RetrainRequest> GetRetrainData(float conf, bool choseApproverd);
        Task UpdateModelAproved(UpdateModelAprovedMV request);
        Task AddOrder(Guid id, Guid OrderId);
        Task UpdateStatus(Guid Id, string Status);
        Task UpdateMecahin(Guid Id, Guid MechanicId);
    }
}
