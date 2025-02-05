using BlazorApp.Data;
using BlazorApp.Extensions.ViewModels.IdentityVMs;

namespace BlazorApp.Services.Interfaces
{
    public interface IUserService
    {
        Task ChangeEmail(Guid Id, string Email);
        Task ConfirmEmail(Guid Id, string Email, string code);
        Task ForgotPasswordAsync(string Email);
        Task ForgotPasswordUnAuthAsync(string Email);
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
        Task<ApplicationUser> GetUser(Guid id);
        Task<ApplicationUser> GetUserByEmail(string email);
        Task<UserViewModel> GetUserWithRole(Guid id);
        Task ResetPassword(Guid Id, string Code, string newPasword);
        Task SetPhoneNumber(UserService.SetPhoneNumberRequest request);
        Task<IEnumerable<MechanicViewModel>> GetMechanics();
        Task<MechanicViewModel> GetMechanic(Guid Id);
        Task DeleteUser(Guid Id);
        Task SaveChanges(UserViewModel modek);
    }
}
