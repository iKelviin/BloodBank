using BloodBank.Application.Models;
using BloodBank.Core.Interfaces;
using MediatR;

namespace BloodBank.Application.Queries.DonorQueries.GetAllDonors;

public class GetAllDonorsQueryHandler : IRequestHandler<GetAllDonorsQuery, ResultViewModel<List<DonorViewModel>>>
{
    private readonly IDonorRepository _repository;
    private readonly IDonationRepository _donationRepository;
    public GetAllDonorsQueryHandler(IDonorRepository repository, IDonationRepository donationRepository)
    {
        _donationRepository = donationRepository;
        _repository = repository;
    }

    public async Task<ResultViewModel<List<DonorViewModel>>> Handle(GetAllDonorsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var donors = await _repository.GetAll();
            var model = donors.Select(DonorViewModel.FromEntity).ToList();
            return ResultViewModel<List<DonorViewModel>>.Success(model);
        }
        catch (Exception e)
        {
            return ResultViewModel<List<DonorViewModel>>.Error($"Error occured: {e.Message}");
        }
    }
}