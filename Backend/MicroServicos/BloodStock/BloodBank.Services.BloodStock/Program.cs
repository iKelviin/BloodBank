using BloodBank.Services.BloodStock.Application;
using BloodBank.Services.BloodStock.Infrastructure;
using BloodBank.Services.Core.BloodStock.Subscribers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddHostedService<BloodApprovedSubscriber>();
builder.Services.AddHostedService<BloodUsedSubscriber>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();