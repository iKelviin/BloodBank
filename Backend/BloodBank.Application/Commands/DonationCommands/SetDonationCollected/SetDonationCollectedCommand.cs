using BloodBank.Application.Models;
using MediatR;

namespace BloodBank.Application.Commands.DonationCommands.SetDonationCollected;

public class SetDonationCollectedCommand : IRequest<ResultViewModel>
{
    public SetDonationCollectedCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}