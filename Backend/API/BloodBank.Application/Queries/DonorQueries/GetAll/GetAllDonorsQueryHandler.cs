using BloodBank.Application.Models;
using BloodBank.Core.Interfaces;
using MediatR;

namespace BloodBank.Application.Queries.DonorQueries.GetAllDonors;

public class GetAllDonorsQueryHandler : IRequestHandler<GetAllDonorsQuery, ResultViewModel<List<DonorDetailsViewModel>>>
{
    private readonly IDonorRepository _repository;

    public GetAllDonorsQueryHandler(IDonorRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel<List<DonorDetailsViewModel>>> Handle(GetAllDonorsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var donors = await _repository.GetAll();
            var model = donors.Select(DonorDetailsViewModel.FromEntity).ToList();
            return ResultViewModel<List<DonorDetailsViewModel>>.Success(model);
        }
        catch (Exception e)
        {
            return ResultViewModel<List<DonorDetailsViewModel>>.Error($"Error occured: {e.Message}");
        }
    }
}