using ServiceStationApp.Extensions.ViewModels.IdentityVMs;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace ServiceStationApp.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<JwtViewModel> SignInAsync(UserSignInViewModel viewModel);
        Task<JwtViewModel> SignUpAsync(UserSignUpViewModel viewModel);
        Task<ClaimsPrincipal> GenerateStateFromToken(JwtSecurityToken token);
        JwtSecurityToken ReadJwt(string tocken);
        Task ResendConfirmationEmailAsync(string Email);
        Task ConfirmEmail(Guid Id, string code);
    }
}
