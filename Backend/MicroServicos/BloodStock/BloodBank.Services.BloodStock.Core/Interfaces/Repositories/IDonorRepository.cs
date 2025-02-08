using BloodBank.Services.BloodStock.Core.Entities;

namespace BloodBank.Services.BloodStock.Core.Interfaces.Repositories;

public interface IDonorRepository
{
    Task<List<Donor>> GetAll();
    Task<Donor> GetById(Guid id);
    Task<bool> Exists(string email);
    Task<Guid> Add(Donor donor);
    Task Update(Donor donor);
}