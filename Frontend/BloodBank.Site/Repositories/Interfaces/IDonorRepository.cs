using BloodBank.Site.Models;

namespace BloodBank.Site.Repositories.Interfaces;

public interface IDonorRepository
{
    Task <ResultViewModel<List<DonorViewModel>>> GetAllDonors();
    Task<ResultViewModel<DonorViewModel>> GetDonorById(Guid id);
    Task<ResultViewModel<Guid>> Add(DonorCreateModel donor);
    Task<ResultViewModel> Update(DonorUpdateModel donor);
    Task<ResultViewModel> Delete(Guid id);
}