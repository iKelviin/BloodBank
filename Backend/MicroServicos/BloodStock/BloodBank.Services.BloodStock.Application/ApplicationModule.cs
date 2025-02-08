using BloodBank.Services.BloodStock.Application.Commands.StockCommands.InsertBloodStock;
using Microsoft.Extensions.DependencyInjection;

namespace BloodBank.Services.BloodStock.Application;


public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddHandlers();
        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
       services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<InsertBloodStockCommand>();
        });
        return services;
    }

}