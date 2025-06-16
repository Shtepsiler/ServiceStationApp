using BlazorApp.Components;
using BlazorApp.Components.Account;
using BlazorApp.Data;
using BlazorApp.Extensions;
using BlazorApp.Services;
using BlazorApp.Services.Interfaces;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Nethereum.Metamask;
using Nethereum.Metamask.Blazor;
using Nethereum.UI;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using ServiceCenterPayment;
using ServiceStationApp.Components.Account;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
string APIBaseString = builder.Configuration["APIBaseString"];
string infuraUrl = builder.Configuration["InfuraUrl"];
string privateKey = builder.Configuration["PrivateKey"];
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddMudServices();
builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();

//builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationEthereumStateProvider>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
    .AddIdentityCookies();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("InMemoryDb"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();


builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IChooseModelService, ChooseModelService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IMachineLerningService, MachineLerningService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPartsService, PartsService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ISpecialisationsService, SpecialisationsService>();
builder.Services.AddScoped<ITasksService, TasksService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IMechanicsService, MechanicsService>();
builder.Services.AddScoped<IPartsMLService, PartsMLService>();



builder.Services.AddHttpClient("Identity", client =>
{
    client.BaseAddress = new Uri($"{APIBaseString}account/Identity/");
    client.DefaultRequestHeaders.Add("Accept", "text/plain");
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler();
    handler.CookieContainer = new CookieContainer();
    return handler;
});

builder.Services.AddHttpClient("User", client =>
{
    client.BaseAddress = new Uri($"{APIBaseString}account/User/");
    client.DefaultRequestHeaders.Add("Accept", "text/plain");
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler();
    handler.CookieContainer = new CookieContainer();
    return handler;
});

builder.Services.AddHttpClient("Role", client =>
{
    client.BaseAddress = new Uri($"{APIBaseString}account/Role/");
    client.DefaultRequestHeaders.Add("Accept", "text/plain");
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler();
    handler.CookieContainer = new CookieContainer();
    return handler;
});

builder.Services.AddHttpClient("Job", client =>
{
    client.BaseAddress = new Uri($"{APIBaseString}jobs/Job/");
    client.DefaultRequestHeaders.Add("Accept", "text/plain");
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler();
    handler.CookieContainer = new CookieContainer();
    return handler;
});

builder.Services.AddHttpClient("Task", client =>
{
    client.BaseAddress = new Uri($"{APIBaseString}jobs/MechanicsTasks/");
    client.DefaultRequestHeaders.Add("Accept", "text/plain");
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler();
    handler.CookieContainer = new CookieContainer();
    return handler;
});

builder.Services.AddHttpClient("Category", client =>
{
    client.BaseAddress = new Uri($"{APIBaseString}parts/Category/");
    client.DefaultRequestHeaders.Add("Accept", "text/plain");
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler();
    handler.CookieContainer = new CookieContainer();
    return handler;
});

builder.Services.AddHttpClient("Order", client =>
{
    client.BaseAddress = new Uri($"{APIBaseString}parts/Order/");
    client.DefaultRequestHeaders.Add("Accept", "text/plain");
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler();
    handler.CookieContainer = new CookieContainer();
    return handler;
});

builder.Services.AddHttpClient("Part", client =>
{
    client.BaseAddress = new Uri($"{APIBaseString}parts/Part/");
    client.DefaultRequestHeaders.Add("Accept", "text/plain");
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler();
    handler.CookieContainer = new CookieContainer();
    return handler;
});
builder.Services.AddHttpClient("Vehicle", client =>
{
    client.BaseAddress = new Uri($"{APIBaseString}parts/Vehicle/");
    client.DefaultRequestHeaders.Add("Accept", "text/plain");
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler();
    handler.CookieContainer = new CookieContainer();
    return handler;
});
builder.Services.AddHttpClient("Parts", client =>
{
    client.BaseAddress = new Uri($"{APIBaseString}parts/");
    client.DefaultRequestHeaders.Add("Accept", "text/plain");
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler();
    handler.CookieContainer = new CookieContainer();
    return handler;
});
builder.Services.AddHttpClient("Model", client =>
{
    client.BaseAddress = new Uri($"{APIBaseString}specialisaton-model/");
    client.DefaultRequestHeaders.Add("Accept", "text/plain");
    client.Timeout = TimeSpan.FromSeconds(60); // Increased timeout for ML operations

})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler();
    handler.CookieContainer = new CookieContainer();
    return handler;
});
builder.Services.AddHttpClient("PPModel", client =>
    {
        client.BaseAddress = new Uri($"{APIBaseString}parts-model/");
        client.DefaultRequestHeaders.Add("Accept", "text/plain");
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        client.Timeout = TimeSpan.FromSeconds(60); // Increased timeout for ML operations
        client.DefaultRequestHeaders.Add("User-Agent", "BlazorApp/1.0");
    })
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        var handler = new HttpClientHandler();
        handler.CookieContainer = new CookieContainer();
        return handler;
    });
builder.Services.AddHttpClient("Specialisations", client =>
{
    client.BaseAddress = new Uri($"{APIBaseString}jobs/Specialisation/");
    client.DefaultRequestHeaders.Add("Accept", "text/plain");
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler();
    handler.CookieContainer = new CookieContainer();
    return handler;
});

builder.Services.AddHttpClient("Mecahics", client =>
{
    client.BaseAddress = new Uri($"{APIBaseString}jobs/Mecahic/");
    client.DefaultRequestHeaders.Add("Accept", "text/plain");
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler();
    handler.CookieContainer = new CookieContainer();
    return handler;
});
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Environment.WebRootPath) });

builder.Services.AddTransient<ApiHttpClient>();





//// Add Ethereum/Nethereum services

builder.Services.AddScoped<Web3>(sp =>
{
    if (!string.IsNullOrEmpty(privateKey))
    {
        var account = new Account(privateKey);
        return new Web3(account, infuraUrl);
    }
    return new Web3(infuraUrl);
});
builder.Services.AddScoped<IWeb3, Web3>(sp =>
{
    if (!string.IsNullOrEmpty(privateKey))
    {
        var account = new Account(privateKey);
        return new Web3(account, infuraUrl);
    }
    return new Web3(infuraUrl);
});


builder.Services.AddScoped<IEthereumHostProvider, MetamaskHostProvider>();
builder.Services.AddScoped<IMetamaskInterop, MetamaskBlazorInterop>();
builder.Services.AddScoped<MetamaskHostProvider>();
builder.Services.AddScoped<SelectedEthereumHostProviderService>();
builder.Services.Configure<Web3Config>(builder.Configuration.GetSection("Blockchain"));
builder.Services.AddWeb3Context();
builder.Services.AddSingleton<IServiceCenterPaymentServiceFactory, ServiceCenterPaymentServiceFactory>();

builder.Services.AddScoped(services =>
{
    var metamaskHostProvider = services.GetService<MetamaskHostProvider>();
    var selectedHostProvider = new SelectedEthereumHostProviderService();
    selectedHostProvider.SetSelectedEthereumHostProvider(metamaskHostProvider);
    return selectedHostProvider;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseStaticFiles();
app.UseAntiforgery();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorApp.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
