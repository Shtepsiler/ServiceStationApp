using BlazorApp.Client;
using BlazorApp.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Nethereum.UI;
using System.Diagnostics;
using System.Security.Claims;

namespace ServiceStationApp.Components.Account
{
    public sealed class PersistingRevalidatingAuthenticationEthereumStateProvider : RevalidatingServerAuthenticationStateProvider
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly PersistentComponentState state;
        private readonly IdentityOptions options;
        private readonly PersistingComponentStateSubscription subscription;
        private readonly SelectedEthereumHostProviderService selectedHostProviderService;
        private IEthereumHostProvider ethereumHostProvider;

        private Task<AuthenticationState>? authenticationStateTask;

        public PersistingRevalidatingAuthenticationEthereumStateProvider(
            ILoggerFactory loggerFactory,
            IServiceScopeFactory serviceScopeFactory,
            PersistentComponentState persistentComponentState,
            IOptions<IdentityOptions> optionsAccessor,
            SelectedEthereumHostProviderService selectedHostProviderService)
            : base(loggerFactory)
        {
            scopeFactory = serviceScopeFactory;
            state = persistentComponentState;
            options = optionsAccessor.Value;
            this.selectedHostProviderService = selectedHostProviderService;

            AuthenticationStateChanged += OnAuthenticationStateChanged;
            subscription = state.RegisterOnPersisting(OnPersistingAsync, RenderMode.InteractiveWebAssembly);

            // Initialize EthereumHostProvider and listen for changes
            selectedHostProviderService.SelectedHostProviderChanged += SelectedHostProviderChanged;
            InitSelectedHostProvider();
        }

        protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(120);

        // Implement the missing abstract method
        protected override async Task<bool> ValidateAuthenticationStateAsync(
            AuthenticationState authenticationState, CancellationToken cancellationToken)
        {
            if (ethereumHostProvider != null && ethereumHostProvider.Available)
            {
                // If Ethereum host provider is available, assume the state is valid
                return true;
            }

            // Otherwise, validate the regular authentication state
            await using var scope = scopeFactory.CreateAsyncScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            return await ValidateSecurityStampAsync(userManager, authenticationState.User);
        }

        private void InitSelectedHostProvider()
        {
            ethereumHostProvider = selectedHostProviderService.SelectedHost;
            if (ethereumHostProvider != null)
            {
                ethereumHostProvider.SelectedAccountChanged += SelectedAccountChanged;
            }
        }

        private async Task SelectedHostProviderChanged(IEthereumHostProvider newEthereumHostProvider)
        {
            // This method is called when the selected Ethereum host provider changes.
            // Update the Ethereum host provider.
            ethereumHostProvider = newEthereumHostProvider;

            if (ethereumHostProvider != null)
            {
                ethereumHostProvider.SelectedAccountChanged += SelectedAccountChanged;
                // Optionally, handle the change (for example, notify the UI).
            }
            else
            {
                // Handle case where the host provider is null (if needed).
            }
        }

        private async Task SelectedAccountChanged(string ethereumAddress)
        {
            if (string.IsNullOrEmpty(ethereumAddress))
            {
                await NotifyAuthenticationStateAsEthereumDisconnected();
            }
            else
            {
                await NotifyAuthenticationStateAsEthereumConnected(ethereumAddress);
            }
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Отримуємо стандартний AuthenticationState
            var authenticationState = await base.GetAuthenticationStateAsync();
            var principal = authenticationState.User;

            if (ethereumHostProvider != null && ethereumHostProvider.Available)
            {
                var currentAddress = await ethereumHostProvider.GetProviderSelectedAccountAsync();
                if (!string.IsNullOrEmpty(currentAddress))
                {
                    // Додаємо Ethereum-клейми до існуючого ClaimsPrincipal
                    principal = GetClaimsPrincipal(currentAddress, principal);
                }
            }

            return new AuthenticationState(principal);
        }

        private ClaimsPrincipal GetClaimsPrincipal(string ethereumAddress, ClaimsPrincipal existingPrincipal)
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, ethereumAddress),
        new Claim(ClaimTypes.Role, "EthereumConnected")
    };

            var ethereumIdentity = new ClaimsIdentity(claims, "ethereumConnection");

            // Створюємо нового ClaimsPrincipal, який включає обидва ClaimsIdentity
            var mergedPrincipal = new ClaimsPrincipal(existingPrincipal);
            mergedPrincipal.AddIdentity(ethereumIdentity);

            return mergedPrincipal;
        }


        public async Task NotifyAuthenticationStateAsEthereumConnected(string currentAddress)
        {
            var authState = await GetAuthenticationStateAsync();
            var claimsPrincipal = GetClaimsPrincipal(currentAddress, authState.User);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }


        public async Task NotifyAuthenticationStateAsEthereumDisconnected()
        {
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private async Task<bool> ValidateSecurityStampAsync(UserManager<ApplicationUser> userManager, ClaimsPrincipal principal)
        {
            var user = await userManager.GetUserAsync(principal);
            if (user is null)
            {
                return false;
            }
            else if (!userManager.SupportsUserSecurityStamp)
            {
                return true;
            }
            else
            {
                var principalStamp = principal.FindFirstValue(options.ClaimsIdentity.SecurityStampClaimType);
                var userStamp = await userManager.GetSecurityStampAsync(user);
                return principalStamp == userStamp;
            }
        }

        private void OnAuthenticationStateChanged(Task<AuthenticationState> task)
        {
            authenticationStateTask = task;
        }

        private async Task OnPersistingAsync()
        {
            if (authenticationStateTask is null)
            {
                throw new UnreachableException($"Authentication state not set in {nameof(OnPersistingAsync)}().");
            }

            var authenticationState = await authenticationStateTask;
            var principal = authenticationState.User;

            if (principal.Identity?.IsAuthenticated == true)
            {
                var userId = principal.FindFirst(options.ClaimsIdentity.UserIdClaimType)?.Value;
                var email = principal.FindFirst(options.ClaimsIdentity.EmailClaimType)?.Value;
                var username = principal.FindFirst(options.ClaimsIdentity.UserNameClaimType)?.Value;
                var role = principal.FindFirst(options.ClaimsIdentity.RoleClaimType)?.Value;

                if (userId != null && email != null)
                {
                    state.PersistAsJson(nameof(UserInfo), new UserInfo
                    {
                        UserId = userId,
                        Email = email,
                        UserName = username,
                        Role = role,
                    });
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            subscription.Dispose();
            AuthenticationStateChanged -= OnAuthenticationStateChanged;
            base.Dispose(disposing);

            if (ethereumHostProvider != null)
            {
                ethereumHostProvider.SelectedAccountChanged -= SelectedAccountChanged;
            }

            if (selectedHostProviderService != null)
            {
                selectedHostProviderService.SelectedHostProviderChanged -= SelectedHostProviderChanged;
            }
        }
    }
}
