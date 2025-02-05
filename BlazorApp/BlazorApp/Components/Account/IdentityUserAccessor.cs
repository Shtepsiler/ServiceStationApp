using BlazorApp.Data;
using Microsoft.AspNetCore.Identity;
using System.Net.NetworkInformation;

using BlazorApp.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Security.Claims;
namespace BlazorApp.Components.Account
{
    internal sealed class IdentityUserAccessor(IdentityRedirectManager redirectManager, IUserService userServisce)
    {
        public async Task<ApplicationUser> GetRequiredUserAsync(HttpContext context)
        {
            var user = await userServisce.GetUser(Guid.Parse(context.User.FindFirst(new ClaimsIdentityOptions().UserIdClaimType)?.Value));

            if (user is null)
            {
                redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user with ID '{context.User.FindFirst(new ClaimsIdentityOptions().UserIdClaimType)?.Value}'.", context);
            }

            if(!user.EmailConfirmed)
            {
                redirectManager.RedirectTo("/Account/ConfirmEmail");
            }




            return user;
        }
    }
}
