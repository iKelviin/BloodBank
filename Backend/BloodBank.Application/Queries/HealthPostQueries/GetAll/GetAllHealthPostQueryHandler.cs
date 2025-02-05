using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using BloodBank.Core.Interfaces;
using MediatR;

namespace BloodBank.Application.Queries.HealthPostQueries.GetAll;

public class GetAllHealthPostQueryHandler : IRequestHandler<GetAllHealthPostQuery, ResultViewModel<List<HealthPost>>>
{
    private readonly IHealthPostRepository _healthPostRepository;

    public GetAllHealthPostQueryHandler(IHealthPostRepository healthPostRepository)
    {
        _healthPostRepository = healthPostRepository;
    }

    public async Task<ResultViewModel<List<HealthPost>>> Handle(GetAllHealthPostQuery request, CancellationToken cancellationToken)
    {
        var locations = await _healthPostRepository.GetAll();
        return new ResultViewModel<List<HealthPost>>(locations);
    }
}