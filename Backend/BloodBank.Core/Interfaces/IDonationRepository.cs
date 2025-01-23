using BloodBank.Core.Entities;

namespace BloodBank.Core.Interfaces;

public interface IDonationRepository
{
    Task<List<Donation>> GetAll();
    Task<List<Donation>> GetByDonorId(Guid donorId);
    Task<Donation> GetById(Guid id);
    Task<Guid> Add(Donation donation);
    Task Update(Donation donation);
}