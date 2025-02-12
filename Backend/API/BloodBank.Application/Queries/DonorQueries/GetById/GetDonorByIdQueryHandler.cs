using BloodBank.Application.Models;
using BloodBank.Core.Interfaces;
using MediatR;

namespace BloodBank.Application.Queries.DonorQueries.GetById;

public class GetDonorByIdQueryHandler : IRequestHandler<GetDonorByIdQuery, ResultViewModel<DonorDetailsViewModel>>
{
    private readonly IDonorRepository _repository;
    private readonly IDonationRepository _donationRepository;

    public GetDonorByIdQueryHandler(IDonorRepository repository, IDonationRepository donationRepository)
    {
        _donationRepository = donationRepository;
        _repository = repository;
    }

    public async Task<ResultViewModel<DonorDetailsViewModel>> Handle(GetDonorByIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var donor = await _repository.GetById(request.Id);
            if (donor is null) return ResultViewModel<DonorDetailsViewModel>.Error("Donor not found");

            var model = DonorDetailsViewModel.FromEntity(donor);
            var lastDonation = await _donationRepository.GetLastByDonorId(request.Id);
            if (lastDonation != null)
            {
                model.LastDonation = lastDonation.DonationDate.ToShortDateString();
            }

            return ResultViewModel<DonorDetailsViewModel>.Success(model);
        }
        catch (Exception e)
        {
            return ResultViewModel<DonorDetailsViewModel>.Error($"Error occured: {e.Message}");
        }
    }
}