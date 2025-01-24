using BloodBank.Application.Models;
using MediatR;

namespace BloodBank.Application.Queries.DonorQueries.GetById;

public class GetDonorByIdQuery : IRequest<ResultViewModel<DonorDetailsViewModel>>
{
    public GetDonorByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}