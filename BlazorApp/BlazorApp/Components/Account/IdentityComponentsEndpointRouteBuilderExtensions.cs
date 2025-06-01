using BlazorApp.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Components.Account
{
    internal static class IdentityComponentsEndpointRouteBuilderExtensions
    {
        // These endpoints are required by the Identity Razor components defined in the /Components/Account/Pages directory of this project.
        public static IEndpointConventionBuilder MapAdditionalIdentityEndpoints(this IEndpointRouteBuilder endpoints)
        {
            ArgumentNullException.ThrowIfNull(endpoints);

            var accountGroup = endpoints.MapGroup("/Account");
            accountGroup.MapPost("/Logout", async (
                ApiHttpClient apiHttpClient,
                [FromForm] string returnUrl) =>
            {
                await apiHttpClient.SignOut();
                return TypedResults.Redirect($"/");
            });

            return accountGroup;
        }
    }
}
