using BloodBank.Application.Models;
using BloodBank.Core.Interfaces;
using MediatR;

namespace BloodBank.Application.Queries.DonationQueries.GetByDonorId;

public class GetDonationsByDonorIdQueryHandler : IRequestHandler<GetDonationsByDonorIdQuery, ResultViewModel<List<DonationViewModel>>>
{
    private readonly IDonationRepository _donationRepository;

    public GetDonationsByDonorIdQueryHandler(IDonationRepository donationRepository)
    {
        _donationRepository = donationRepository;
    }

    public async Task<ResultViewModel<List<DonationViewModel>>> Handle(GetDonationsByDonorIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var donations = await _donationRepository.GetByDonorId(request.Id);
            var model = donations.Select(DonationViewModel.FromEntity).ToList();
            return ResultViewModel<List<DonationViewModel>>.Success(model);
        }
        catch (Exception e)
        {
            return ResultViewModel<List<DonationViewModel>>.Error($"Error occured: {e.Message}");
        }
    }
}