using BloodBank.Application.Commands.DonorCommands.InsertDonor;
using BloodBank.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace BloodBank.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddHandlers()
            .AddValidation();
        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<InsertDonorCommand>();
        });
        return services;
    }

    private static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssemblyContaining<InsertDonorValidator>();
        return services;
    }
}