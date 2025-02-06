using BloodBank.Application.Models;
using BloodBank.Core.Interfaces;
using MediatR;

namespace BloodBank.Application.Queries.DonorQueries.GetById;

public class GetDonorByIdQueryHandler : IRequestHandler<GetDonorByIdQuery, ResultViewModel<DonorDetailsViewModel>>
{
    private readonly IDonorRepository _repository;

    public GetDonorByIdQueryHandler(IDonorRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel<DonorDetailsViewModel>> Handle(GetDonorByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var donor = await _repository.GetById(request.Id);
            if (donor is null) return ResultViewModel<DonorDetailsViewModel>.Error("Donor not found");

            var model = DonorDetailsViewModel.FromEntity(donor);

            return ResultViewModel<DonorDetailsViewModel>.Success(model);
        }
        catch (Exception e)
        {
            return ResultViewModel<DonorDetailsViewModel>.Error($"Error occured: {e.Message}");
        }
    }
}