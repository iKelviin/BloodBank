using BloodBank.Site.Models;

namespace BloodBank.Site.Services.Interfaces;

public interface IDonorService
{
    Task<ResultViewModel<ListPagedModel<DonorViewModel>>> GetAllDonors(int currentPage, int pageSize,string? searchWord = null);
    Task<ResultViewModel<DonorDetailsViewModel>> GetDonorById(Guid id);
    Task<ResultViewModel<Guid>> Add(DonorCreateModel donor);
    Task<ResultViewModel> Update(DonorUpdateModel donor);
    Task<ResultViewModel> Delete(Guid id);
}