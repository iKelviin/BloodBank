using BloodBank.Application.Commands.AuthCommands.RegisterCommand;
using BloodBank.Application.Commands.DonorCommands.InsertDonor;
using BloodBank.Core.Enums;
using FluentValidation;

namespace BloodBank.Application.Validators;

public class RegisterDonorValidator : AbstractValidator<RegisterDonorCommand>
{
    public RegisterDonorValidator()
    {
        RuleFor(d => d.FullName)
            .NotEmpty()
            .WithMessage("Full Name is required.");
        
        RuleFor(d => d.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Email is invalid");
        
        RuleFor(d => d.BirthDay)
            .LessThan(DateTime.Now)
            .WithMessage("Birthday must be in the past.");

        RuleFor(d => d.Gender)
            .Must(value => Enum.TryParse<GenderEnum>(value, true, out _))
            .WithMessage("Gender must be a valid value (Male or Female).");
        
        RuleFor(d => d.Weight)
            .GreaterThan(0)
            .WithMessage("Weight must be greater than 0.");

        RuleFor(d => d.BloodType)
            .NotEmpty()
            .WithMessage("Blood type is required")
            .Must(value => Enum.TryParse<BloodTypeEnum>(value, true, out _))
            .WithMessage("Blood type is invalid. Allowed values: A, B, AB, O");

        RuleFor(d => d.RhFactor)
            .NotEmpty()
            .WithMessage("RhFactor is required")
            .Must(value => Enum.TryParse<RhFactorEnum>(value, true, out _))
            .WithMessage("RhFactor is invalid. Allowed values: Positive, Negative");

        RuleFor(d => d.Street)
            .NotEmpty()
            .WithMessage("Street is required.");

        RuleFor(d => d.City)
            .NotEmpty()
            .WithMessage("City is required.");

        RuleFor(d => d.State)
            .NotEmpty()
            .WithMessage("State is required.");
        
        RuleFor(d => d.ZipCode)
            .NotEmpty()
            .WithMessage("Zip code is required")
            .Matches(@"^\d{5}-?\d{3}$")
            .WithMessage("Zip code is invalid. It must be in the format XXXXX-XXX or XXXXXXXX.");

    }
}