using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using BloodBank.Core.Interfaces;
using MediatR;

namespace BloodBank.Application.Queries.HealthPostQueries.GetAll;

public class GetAllHealthPostQueryHandler : IRequestHandler<GetAllHealthPostQuery, ResultViewModel<List<HealthPostViewModel>>>
{
    private readonly IHealthPostRepository _healthPostRepository;

    public GetAllHealthPostQueryHandler(IHealthPostRepository healthPostRepository)
    {
        _healthPostRepository = healthPostRepository;
    }

    public async Task<ResultViewModel<List<HealthPostViewModel>>> Handle(GetAllHealthPostQuery request, CancellationToken cancellationToken)
    {
        var locations = await _healthPostRepository.GetAll();
        var model = locations.Select(HealthPostViewModel.FromEntity).ToList();
        return new ResultViewModel<List<HealthPostViewModel>>(model);
    }
}