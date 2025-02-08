using BloodBank.Services.BloodStock.Core.Entities;

namespace BloodBank.Services.BloodStock.Core.Interfaces.Repositories;

public interface IDonationRepository
{
    Task<List<Donation>> GetAll();
    Task<List<Donation>> GetAllByBloodType(string bloodType, string rhFactor);
    Task<List<Donation>> GetByDonorId(Guid donorId);
    Task<Donation> GetLastByDonorId(Guid donorId);
    Task<Donation> GetById(Guid id);
    Task<Guid> Add(Donation donation);
    Task Update(Donation donation);
}