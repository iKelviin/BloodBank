using BloodBank.Services.Core.BloodStockEntities;

namespace BloodBank.Services.Core.BloodStock.Interfaces;

public interface IDonorRepository
{
    Task<List<Donor>> GetAll();
    Task<Donor> GetById(Guid id);
    Task<bool> Exists(string email);
    Task<Guid> Add(Donor donor);
    Task Update(Donor donor);
}