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

    public async Task<ResultViewModel<List<DonorViewModel>>> GetAllDonors(string? searchWord = null)
    {
        var result = await _repository.GetAllDonors();
        if(!result.IsSucces) return ResultViewModel<List<DonorViewModel>>.Error(result.Message);

        var donors = result.Data!;
        if (!string.IsNullOrEmpty(searchWord))
        {
            donors = donors.FindAll(x=> x.FullName.ToLower().Contains(searchWord.ToLower()));
        }
       
        return ResultViewModel<List<DonorViewModel>>.Success(donors);
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