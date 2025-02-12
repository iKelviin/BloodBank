using BloodBank.Site.Models;
using BloodBank.Site.Models.HealthPost;

namespace BloodBank.Site.Services.Interfaces;

public interface IHealthPostService
{
    Task<ResultViewModel<List<HealthPostViewModel>>> GetAll();
}