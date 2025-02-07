using BloodBank.Services.BloodAnalysis.Commands.DonationCommands;
using Microsoft.Extensions.DependencyInjection;

namespace BloodBank.Services.BloodAnalysis.Core;


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
            config.RegisterServicesFromAssemblyContaining<SetDonationApprovedCommand>();
        });
        return services;
    }
}