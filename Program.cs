using ServiceStationApp.Components;
using ServiceStationApp.Components.Account;
using ServiceStationApp.Data;
using ServiceStationApp.Extensions;
using ServiceStationApp.Services;
using ServiceStationApp.Services.Interfaces;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using MudBlazor.Services;
using System.Net;
//using Nethereum.Blazor;
//using Nethereum.Metamask.Blazor;
using Nethereum.Metamask;
using Nethereum.UI;
using FluentValidation;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Microsoft.EntityFrameworkCore;
using ServiceCenterPayment;
using ServiceStationApp.Components.Info;
using Microsoft.Extensions.DependencyInjection;

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
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
//builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();
builder.Services.AddAuthenticationCore();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
    .AddIdentityCookies();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("InMemoryDb")); 
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
    client.BaseAddress = new Uri($"{APIBaseString}model/");
    client.DefaultRequestHeaders.Add("Accept", "text/plain");
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

builder.Services.AddTransient<ApiHttpClient>();

builder.Services.AddServerSideBlazor();
builder.Services.AddAuthorizationCore();


builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("") });
builder.Services.AddScoped<AuthenticationStateProvider,PersistingRevalidatingAuthenticationStateProvider>();



//Add metamask as the selected ethereum host provider
//builder.Services.AddScoped(services =>
//{
//    var metamaskHostProvider = services.GetService<MetamaskHostProvider>();
//    var selectedHostProvider = new SelectedEthereumHostProviderService();
//    selectedHostProvider.SetSelectedEthereumHostProvider(metamaskHostProvider);
//    return selectedHostProvider;
//});

//builder.Services.AddScoped<Web3>(sp =>
//{
//    if (!string.IsNullOrEmpty(privateKey))
//    {
//        var account = new Account(privateKey);
//        return new Web3(account, infuraUrl);
//    }
//    return new Web3(infuraUrl);
//});





//// Add Ethereum/Nethereum services
//builder.Services.AddScoped<IEthereumHostProvider, MetamaskHostProvider>();
//builder.Services.AddScoped<IMetamaskInterop, MetamaskBlazorInterop>();
//builder.Services.AddScoped<MetamaskHostProvider>();
//builder.Services.AddScoped<SelectedEthereumHostProviderService>();
//builder.Services.Configure<Web3Config>(builder.Configuration.GetSection("Blockchain"));
//builder.Services.AddWeb3Context();
//builder.Services.AddSingleton<IServiceCenterPaymentServiceFactory, ServiceCenterPaymentServiceFactory>();




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


    app.MapRazorComponents<ServiceStationApp.Components.App>()
        .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
