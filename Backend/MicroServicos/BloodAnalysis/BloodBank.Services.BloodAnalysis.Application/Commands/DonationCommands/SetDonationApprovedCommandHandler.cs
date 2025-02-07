using BloodBank.Services.BloodAnalysis.Core.Interfaces;
using BloodBank.Services.BloodAnalysis.Models;
using MediatR;

namespace BloodBank.Services.BloodAnalysis.Commands.DonationCommands;

public class SetDonationApprovedCommandHandler : IRequestHandler<SetDonationApprovedCommand, ResultViewModel<DonationApprovedViewModel>>
{
    private readonly IDonationRepository _donationRepository;

    public SetDonationApprovedCommandHandler(IDonationRepository donationRepository)
    {
        _donationRepository = donationRepository;
    }

    public async Task<ResultViewModel<DonationApprovedViewModel>> Handle(SetDonationApprovedCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var donation = await _donationRepository.GetById(request.DonationId);
            if (donation is null)
                return ResultViewModel<DonationApprovedViewModel>.Error("Donation not found");

            donation.SetAsAnalyzed();
            await _donationRepository.Update(donation);
            await Task.Delay(10000, cancellationToken);

            donation.SetAsApproved();
            await _donationRepository.Update(donation);
            
            var model = DonationApprovedViewModel.FromEntity(donation);

            return ResultViewModel<DonationApprovedViewModel>.Success(model);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error occured: {e.Message}");
            return ResultViewModel<DonationApprovedViewModel>.Error($"Error occured: {e.Message}");
        }
    }
}