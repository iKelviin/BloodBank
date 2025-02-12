using BloodBank.Site.Models;
using BloodBank.Site.Models.HealthPost;
using BloodBank.Site.Repositories.Interfaces;
using BloodBank.Site.Services.Interfaces;

namespace BloodBank.Site.Services;

public class HealthPostService : IHealthPostService
{
    private readonly IHealthPostRepository _repository;

    public HealthPostService(IHealthPostRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel<List<HealthPostViewModel>>> GetAll()
    {
        return await _repository.GetAll();
    }
}