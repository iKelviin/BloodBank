using BloodBank.Services.BloodAnalysis.Models;
using MediatR;

namespace BloodBank.Services.BloodAnalysis.Commands.DonationCommands;

public class SetDonationApprovedCommand : IRequest<ResultViewModel<DonationApprovedViewModel>>
{
    public SetDonationApprovedCommand(Guid donationId)
    {
        DonationId = donationId;
    }

    public Guid DonationId { get; set; }
}