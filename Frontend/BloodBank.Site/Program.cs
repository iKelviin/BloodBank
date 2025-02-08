using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BloodBank.Site;
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


// Shared JS Components
builder.Services.AddScoped<Toastify>();
builder.Services.AddScoped<SweetAlert>();

await builder.Build().RunAsync();