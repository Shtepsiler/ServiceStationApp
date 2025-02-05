using Azure.Core;
using BlazorApp.Components.Account.Pages.Manage;
using BlazorApp.Data;
using BlazorApp.Extensions;
using BlazorApp.Extensions.ViewModels.IdentityVMs;
using BlazorApp.Services.Interfaces;
using Blazorise;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Services
{
    public class UserService : IUserService
    {
        private readonly ApiHttpClient httpClient;

        public UserService(IHttpClientFactory clientFactory, ApiHttpClient httpClient)
        {

            this.httpClient = httpClient.SetHttpClient(clientFactory.CreateClient("User"));

        }


        public async Task <IEnumerable<UserViewModel>> GetAllUsersAsync()
        {
            var user = await httpClient.GetAsync<IEnumerable<UserViewModel>>("AllUsers");

            return user;

        }



        public async Task<ApplicationUser> GetUser(Guid id)
        {
            

            var user = await httpClient.GetAsync<ApplicationUser>(id.ToString());

            return user;

        }

        public async Task<UserViewModel> GetUserWithRole(Guid id)
        {
            var user = await httpClient.GetAsync<UserViewModel>($"GetWithRole/{id.ToString()}");

            return user;

        }
        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            var parameters = new Dictionary<string, string>
            {{"Email",$"{email}" } }; 

            var user = await httpClient.GetAsync<ApplicationUser>("GetByEmail", parameters);

            return user;

        }
        public async Task ForgotPasswordAsync(string Email)
        {
            var parameters = new Dictionary<string, string>
            {{"Email",$"{Email}" } };


            await httpClient.PostAsync("ForgotPassword", parameters);


        }
        public async Task ForgotPasswordUnAuthAsync(string Email)
        {
            var parameters = new Dictionary<string, string>
            {{"Email",$"{Email}" } };


            await httpClient.PostAsync("ForgotPasswordUnAuth", parameters);


        }
        public async Task ResetPassword(Guid Id, string Code, string newPasword)
        {
            var parameters = new Dictionary<string, string>
            {{"Id",$"{Id}" },
                {"Code",$"{Code}" } };


            await httpClient.PostAsync("ResetPassword", parameters, newPasword);

        }

        public class SetPhoneNumberRequest
        {
            public Guid Id { get; set; }
            public string PhoneNumber { get; set; }
        }

        public async Task SetPhoneNumber(SetPhoneNumberRequest request)
        {
            var parameters = new Dictionary<string, string> {
                {"Id",$"{request.Id.ToString()}" }
            };

            await httpClient.PutAsync($"SetPhoneNumber/{request.Id.ToString()}", request.PhoneNumber);

        }

        public async Task ChangeEmail(Guid Id, string Email)
        {

            var parameters = new Dictionary<string, string> {
                {"Id",$"{Id.ToString()}" }
            };

            await httpClient.PutAsync("ChangeEmail", parameters,Email); 

        }





        public async Task ConfirmEmail(Guid Id, string Email,string code)
        {
            var parameters = new Dictionary<string, string> {
                {"Id",$"{Id.ToString()}" },   
                {"Email",$"{Email}" },      
                {"Code",$"{code}" },
            };

            await httpClient.PostAsync("ChangeEmail", parameters);


        }

        public async Task DeleteUser(Guid Id)
        {
            await httpClient.DeleteAsync(Id.ToString());

        }

        public async Task SaveChanges(UserViewModel model)
        {
            var parameters = new Dictionary<string, string> {
            };
            await httpClient.PutAsync($"{model.Id.ToString()}", parameters, model);
        }

        public async Task<IEnumerable<MechanicViewModel>> GetMechanics()
        {
            return await httpClient.GetAsync<IEnumerable<MechanicViewModel>>("GetMechanics");
        }

        public async Task<MechanicViewModel> GetMechanic(Guid Id)
        {
            return await httpClient.GetAsync<MechanicViewModel>($"GetMechanic?Id={Id.ToString()}");
        }
    }










    

    }

