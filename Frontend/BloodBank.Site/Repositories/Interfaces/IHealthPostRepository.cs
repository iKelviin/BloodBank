using BloodBank.Site.Models;
using BloodBank.Site.Models.HealthPost;

namespace BloodBank.Site.Repositories.Interfaces;

public interface IHealthPostRepository
{
    Task <ResultViewModel<List<HealthPostViewModel>>> GetAll();
}