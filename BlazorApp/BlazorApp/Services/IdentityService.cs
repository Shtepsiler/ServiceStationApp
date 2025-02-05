using BlazorApp.Components.Account.Pages.Manage;
using BlazorApp.Extensions;
using BlazorApp.Extensions.ViewModels.IdentityVMs;
using BlazorApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using MudBlazor;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Security.Claims;


namespace BlazorApp.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly ApiHttpClient httpClient;

        public IdentityService(IHttpClientFactory clientFactory, ApiHttpClient httpClient)
        {
            this.httpClient = httpClient.SetHttpClient(clientFactory.CreateClient("Identity")); 
        }
        public async Task<JwtViewModel> SignUpAsync(UserSignUpViewModel viewModel)
        {
            var response = await httpClient.PostAsync<UserSignUpViewModel, JwtViewModel>("signUp", viewModel);
            httpClient.SetJwtToken(response.token);
            return response;
        }
        public async Task<JwtViewModel> SignInAsync(UserSignInViewModel viewModel)
        {
            var response = await httpClient.PostLoginAsync<UserSignInViewModel, JwtViewModel>("signIn", viewModel);
            httpClient.SetJwtToken(response.token);
            return response;
        }
        public async Task ResendConfirmationEmailAsync(string Email)
        {
            var parameters = new Dictionary<string, string> { };
            await httpClient.PutAsync("ResendConfirmationEmail",Email);
        }
        public async Task ConfirmEmail(Guid Id, string code)
        {
            var parameters = new Dictionary<string, string>
            {
                {"Id",$"{Id.ToString()}" } ,
                {"Code",$"{code}" } ,
            };
            await httpClient.PostAsync("ConfirmEmail", parameters);
        }
        public JwtSecurityToken ReadJwt(string tocken)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(tocken);
        }
        public async Task<ClaimsPrincipal> GenerateStateFromToken(JwtSecurityToken token)
        {
            var identity = new ClaimsIdentity(token.Claims, "apiauth_type");
            var principal = new ClaimsPrincipal(identity);
            return principal;
        }
    }
}
