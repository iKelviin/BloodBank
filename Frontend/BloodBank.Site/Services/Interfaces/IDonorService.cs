using BloodBank.Site.Models;

namespace BloodBank.Site.Services.Interfaces;

public interface IDonorService
{
    Task<ResultViewModel<List<DonorViewModel>>> GetAllDonors(string? searchWord = null);
    Task<ResultViewModel<DonorViewModel>> GetDonorById(Guid id);
    Task<ResultViewModel<Guid>> Add(DonorCreateModel donor);
    Task<ResultViewModel> Update(DonorUpdateModel donor);
    Task<ResultViewModel> Delete(Guid id);
}