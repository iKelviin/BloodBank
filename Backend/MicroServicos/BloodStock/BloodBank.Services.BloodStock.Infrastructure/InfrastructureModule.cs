using BloodBank.Services.BloodStock.Core.Interfaces;
using BloodBank.Services.BloodStock.Core.Interfaces.Repositories;
using BloodBank.Services.BloodStock.Infrastructure.Persistence;
using BloodBank.Services.Core.BloodStock.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BloodBank.Services.BloodStock.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddData(configuration)
            .AddMessageBus()
            .AddUnitOfWork()
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

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
    
    public static IServiceCollection AddMessageBus(this IServiceCollection services)
    {
        services.AddScoped<IBusService, RabbitMqClientService>();
        return services;
    }
}