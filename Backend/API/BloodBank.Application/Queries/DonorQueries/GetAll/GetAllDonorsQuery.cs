using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using MediatR;

namespace BloodBank.Application.Queries.DonorQueries.GetAllDonors;

public class GetAllDonorsQuery : IRequest<ResultViewModel<List<DonorDetailsViewModel>>>
{
    
}