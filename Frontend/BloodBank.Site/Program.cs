using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BloodBank.Site;
using BloodBank.Site.Notifications;
using BloodBank.Site.Repositories;
using BloodBank.Site.Repositories.Interfaces;
using BloodBank.Site.Services;
using BloodBank.Site.Services.Interfaces;
using BloodBank.Site.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); // Defina a URL base para a API
});

builder.Services.AddScoped<IDonorService, DonorService>();
builder.Services.AddScoped<IDonorRepository, DonorRepository>();
builder.Services.AddScoped<IHealthPostService, HealthPostService>();
builder.Services.AddScoped<IHealthPostRepository, HealthPostRepository>();

builder.Services.AddSingleton<ICepService, CepService>();


// Shared JS Components
builder.Services.AddScoped<Toastify>();
builder.Services.AddScoped<SweetAlert>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<ThemeNotification>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();