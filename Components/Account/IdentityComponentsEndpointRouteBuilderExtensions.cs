using ServiceStationApp.Components.Account.Pages;
using ServiceStationApp.Components.Account.Pages.Manage;
using ServiceStationApp.Data;
using ServiceStationApp.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using ServiceStationApp.Extensions;
using System.Security.Claims;
using System.Text.Json;

namespace ServiceStationApp.Components.Account
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
