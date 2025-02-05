namespace BlazorApp.Services.Interfaces
{
    public interface IRoleService
    {
        Task ReAsignRole(Guid Id,string Rolename);
    }
}
