using BloodBank.Site.Models;
using BloodBank.Site.Repositories.Interfaces;
using BloodBank.Site.Services.Interfaces;

namespace BloodBank.Site.Services;

public class DonorService : IDonorService
{
    private readonly IDonorRepository _repository;

    public DonorService(IDonorRepository donorRepository)
    {
        _repository = donorRepository;
    }

    public async Task<ResultViewModel<ListPagedModel<DonorViewModel>>> GetAllDonors(int currentPage, int pageSize, string? searchWord = null)
    {
        var result = await _repository.GetAllDonors();
        if(!result.IsSucces) return ResultViewModel<ListPagedModel<DonorViewModel>>.Error(result.Message);

        var donors = result.Data!;
        if (!string.IsNullOrEmpty(searchWord))
        {
            donors = donors.FindAll(x=> x.FullName.ToLower().Contains(searchWord.ToLower()));
        }
        var totalPages = (int)Math.Ceiling((double)donors.Count / pageSize);
        var items = donors.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
       
        ListPagedModel<DonorViewModel> pagedList = new ListPagedModel<DonorViewModel>(items, currentPage, totalPages);

        return ResultViewModel<ListPagedModel<DonorViewModel>>.Success(pagedList);
    }

    public async Task<ResultViewModel<DonorViewModel>> GetDonorById(Guid id)
    {
        return await _repository.GetDonorById(id);
    }

    public async Task<ResultViewModel<Guid>> Add(DonorCreateModel donor)
    {
        return await _repository.Add(donor);
    }

    public async Task<ResultViewModel> Update(DonorUpdateModel donor)
    {
        return await _repository.Update(donor);
    }

    public async Task<ResultViewModel> Delete(Guid id)
    {
        return await _repository.Delete(id);
    }
}