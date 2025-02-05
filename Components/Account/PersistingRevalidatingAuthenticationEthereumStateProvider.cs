using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Nethereum.UI;
using ServiceStationApp.Data;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

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
            subscription = state.RegisterOnPersisting(OnPersistingAsync);

            // Initialize EthereumHostProvider and listen for changes
            selectedHostProviderService.SelectedHostProviderChanged += SelectedHostProviderChanged;
            InitSelectedHostProvider();
        }

        protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(30);

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
            if (ethereumHostProvider != null && ethereumHostProvider.Available)
            {
                var currentAddress = await ethereumHostProvider.GetProviderSelectedAccountAsync();
                if (currentAddress != null)
                {
                    var claimsPrincipal = GetClaimsPrincipal(currentAddress);
                    return new AuthenticationState(claimsPrincipal);
                }
            }

            // If Ethereum address is not available, fallback to regular user authentication state
            return await base.GetAuthenticationStateAsync();
        }

        private ClaimsPrincipal GetClaimsPrincipal(string ethereumAddress)
        {
            var claimEthereumAddress = new Claim(ClaimTypes.NameIdentifier, ethereumAddress);
            var claimEthereumConnectedRole = new Claim(ClaimTypes.Role, "EthereumConnected");

            var claimsIdentity = new ClaimsIdentity(new[] { claimEthereumAddress, claimEthereumConnectedRole }, "ethereumConnection");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            return claimsPrincipal;
        }

        public async Task NotifyAuthenticationStateAsEthereumConnected(string currentAddress)
        {
            var claimsPrincipal = GetClaimsPrincipal(currentAddress);
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
                    //state.PersistAsJson(nameof(UserInfo), new UserInfo
                    //{
                    //    UserId = userId,
                    //    Email = email,
                    //    UserName = username,
                    //    Role = role,
                    //});
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
