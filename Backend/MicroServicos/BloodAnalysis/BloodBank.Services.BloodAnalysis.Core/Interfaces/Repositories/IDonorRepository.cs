using BloodBank.Services.BloodAnalysis.Core.Entities;

namespace BloodBank.Services.BloodAnalysis.Core.Interfaces;

public interface IDonorRepository
{
    Task<List<Donor>> GetAll();
    Task<Donor> GetById(Guid id);
    Task<bool> Exists(string email);
    Task<Guid> Add(Donor donor);
    Task Update(Donor donor);
}