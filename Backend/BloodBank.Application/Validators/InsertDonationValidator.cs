using BloodBank.Application.Commands.DonationCommands.InsertDonation;
using FluentValidation;

namespace BloodBank.Application.Validators;

public class InsertDonationValidator : AbstractValidator<InsertDonationCommand>
{
    
    public InsertDonationValidator()
    {
        
        RuleFor(d => d.QuantityMl)
            .InclusiveBetween(420, 470)
            .WithMessage("Quantity blood must be between 420ml and 470ml.");

        RuleFor(d => d.DonorId)
            .NotEmpty()
            .WithMessage("DonorId is required.");

    }
}