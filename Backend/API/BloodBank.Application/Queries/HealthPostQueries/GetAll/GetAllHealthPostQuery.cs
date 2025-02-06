using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using MediatR;

namespace BloodBank.Application.Queries.HealthPostQueries.GetAll;

public class GetAllHealthPostQuery : IRequest<ResultViewModel<List<HealthPostViewModel>>>
{
    
}