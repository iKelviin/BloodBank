using BloodBank.Application.Models;
using BloodBank.Core.Interfaces;
using MediatR;

namespace BloodBank.Application.Queries.DonationQueries.GetAll;

public class GetAllDonationsQueryHandler : IRequestHandler<GetAllDonationsQuery, ResultViewModel<List<DonationViewModel>>>
{
    private readonly IDonationRepository _donationRepository;

    public GetAllDonationsQueryHandler(IDonationRepository donationRepository)
    {
        _donationRepository = donationRepository;
    }

    public async Task<ResultViewModel<List<DonationViewModel>>> Handle(GetAllDonationsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var donations = await _donationRepository.GetAll();
            var model = donations.Select(DonationViewModel.FromEntity).ToList();
            return ResultViewModel<List<DonationViewModel>>.Success(model);
        }
        catch (Exception e)
        {
            return ResultViewModel<List<DonationViewModel>>.Error($"Error occured: {e.Message}");
        }
    }
}