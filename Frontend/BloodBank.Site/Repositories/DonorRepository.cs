using System.Net.Http.Json;
using BloodBank.Site.Models;
using BloodBank.Site.Repositories.Interfaces;

namespace BloodBank.Site.Repositories;

public class DonorRepository : IDonorRepository
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://localhost:7226/api";

    public DonorRepository(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("API");
    }
    
    public async Task<ResultViewModel<List<DonorViewModel>>> GetAllDonors()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/donors");
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return ResultViewModel<List<DonorViewModel>>.Error(error);
            }

            var books = await response.Content.ReadFromJsonAsync<List<DonorViewModel>>();
            return ResultViewModel<List<DonorViewModel>>.Success(books!);
        }
        catch (Exception e)
        {
            return ResultViewModel<List<DonorViewModel>>.Error(e.Message);
        }
    }

    public async Task<ResultViewModel<DonorDetailsViewModel>> GetDonorById(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/donors/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return ResultViewModel<DonorDetailsViewModel>.Error(error);
            }

            var donor = await response.Content.ReadFromJsonAsync<DonorDetailsViewModel>();
            return ResultViewModel<DonorDetailsViewModel>.Success(donor!);
        }
        catch (Exception e)
        {
            return ResultViewModel<DonorDetailsViewModel>.Error(e.Message);
        }
    }

    public async Task<ResultViewModel<Guid>> Add(DonorCreateModel donor)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/donors",donor);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return ResultViewModel<Guid>.Error(error);
            }

            var guid = await response.Content.ReadFromJsonAsync<Guid>();
            return ResultViewModel<Guid>.Success(guid);
        }
        catch (Exception e)
        {
            return ResultViewModel<Guid>.Error(e.Message);
        }
    }

    public async Task<ResultViewModel> Update(DonorUpdateModel donor)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/donors/{donor.Id}", donor);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return ResultViewModel.Error(error);
            }

            return ResultViewModel.Success();
        }
        catch (Exception e)
        {
            return ResultViewModel.Error(e.Message);
        }
    }

    public async Task<ResultViewModel> Delete(Guid id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/donors/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return ResultViewModel.Error(error);
            }

            return ResultViewModel.Success();
        }
        catch (Exception e)
        {
            return ResultViewModel.Error(e.Message);
        }
    }
}