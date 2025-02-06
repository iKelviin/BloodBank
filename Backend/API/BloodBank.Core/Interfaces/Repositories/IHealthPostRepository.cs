using BloodBank.Core.Entities;

namespace BloodBank.Core.Interfaces;

public interface IHealthPostRepository
{
    Task<List<HealthPost>> GetAll();
}