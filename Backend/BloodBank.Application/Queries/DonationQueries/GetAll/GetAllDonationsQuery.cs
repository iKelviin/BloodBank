using BloodBank.Application.Models;
using MediatR;

namespace BloodBank.Application.Queries.DonationQueries.GetAll;

public class GetAllDonationsQuery : IRequest<ResultViewModel<List<DonationViewModel>>>
{
    
}