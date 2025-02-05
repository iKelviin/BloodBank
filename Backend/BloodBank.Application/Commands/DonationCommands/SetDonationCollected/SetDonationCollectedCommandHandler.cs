using BloodBank.Application.Models;
using BloodBank.Core.Interfaces;
using MediatR;

namespace BloodBank.Application.Commands.DonationCommands.SetDonationCollected;

public class SetDonationCollectedCommandHandler : IRequestHandler<SetDonationCollectedCommand, ResultViewModel>
{
    private readonly IDonationRepository _donationRepository;

    public SetDonationCollectedCommandHandler(IDonationRepository donationRepository)
    {
        _donationRepository = donationRepository;
    }

    public async Task<ResultViewModel> Handle(SetDonationCollectedCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var donation = await _donationRepository.GetById(request.Id);
            if (donation == null) return ResultViewModel.Error("Donation not found");

            donation.SetAsCollected();
            await _donationRepository.Update(donation);
            return ResultViewModel.Success();
        }
        catch (Exception e)
        {
            return ResultViewModel.Error($"Error occured: {e.Message}");
        }
    }
}