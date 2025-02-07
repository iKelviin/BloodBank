using BloodBank.Services.BloodAnalysis.Core.Interfaces;
using BloodBank.Services.BloodAnalysis.Infrastructure.Persistence;
using BloodBank.Services.BloodAnalysis.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BloodBank.Services.BloodAnalysis.Infrastructure;


public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddData(configuration)
            .AddMessageBus()
            .AddRepositories();
        return services;
    }

    private static IServiceCollection AddData(this IServiceCollection services, IConfiguration configutarion)
    {
        var connectionString = configutarion.GetConnectionString("BloodBankCS");
        services.AddDbContext<BloodBankDbContext>(o => o.UseSqlServer(connectionString));
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IDonorRepository, DonorRepository>();
        services.AddScoped<IDonationRepository, DonationRepository>();
        services.AddScoped<IStockRepository, StockRepository>();
        return services;
    }

    public static IServiceCollection AddMessageBus(this IServiceCollection services)
    {
        services.AddScoped<IBusService, RabbitMqClientService>();
        return services;
    }
}