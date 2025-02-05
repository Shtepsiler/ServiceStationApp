namespace ServiceStationApp.Services.Interfaces
{
    public interface IRoleService
    {
        Task ReAsignRole(Guid Id, string Rolename);
    }
}
