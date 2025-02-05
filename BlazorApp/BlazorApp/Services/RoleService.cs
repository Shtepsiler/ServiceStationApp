using BlazorApp.Extensions;
using BlazorApp.Services.Interfaces;

namespace BlazorApp.Services
{
    public class RoleService : IRoleService
    {
        private readonly ApiHttpClient httpClient;

        public RoleService(IHttpClientFactory clientFactory, ApiHttpClient httpClient)
        {

            this.httpClient = httpClient.SetHttpClient(clientFactory.CreateClient("Role"));

        }

        public async Task ReAsignRole(Guid Id, string Rolename)
        {
            var parameters = new Dictionary<string, string> 
            {
                {"Id",$"{Id.ToString()}" },
                {"Role",$"{Rolename}" }
            
            };

            await httpClient.PostAsync("ReAssignRole", parameters);
        }
    }
}
