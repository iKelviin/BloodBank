using BloodBank.Core.Interfaces;
using BloodBank.Infrastructure.Persistence;
using BloodBank.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace BloodBank.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        services
            .AddData(configuration)
            .AddUnitOfWork()
            .AddRepositories();
        return services;
    }

    private static IServiceCollection AddData(this IServiceCollection services, IConfiguration configutarion)
    {
        var connectionString = configutarion.GetConnectionString("BloodBankCS");
        services.AddDbContext<BloodBankDbContext>(o=>o.UseSqlServer(connectionString));
        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IDonorRepository, DonorRepository>();
        services.AddScoped<IDonationRepository, DonationRepository>();
        services.AddScoped<IStockRepository, StockRepository>();
        services.AddScoped<IHealthPostRepository, HealthPostRepository>();
        return services;
    }

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}