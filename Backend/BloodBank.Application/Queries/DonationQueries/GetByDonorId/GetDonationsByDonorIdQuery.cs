using BloodBank.Application.Models;
using MediatR;

namespace BloodBank.Application.Queries.DonationQueries.GetByDonorId;

public class GetDonationsByDonorIdQuery : IRequest<ResultViewModel<List<DonationViewModel>>>
{
    public GetDonationsByDonorIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}