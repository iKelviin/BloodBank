using System.Net.Http.Json;
using BloodBank.Site.Models;
using BloodBank.Site.Models.HealthPost;
using BloodBank.Site.Repositories.Interfaces;

namespace BloodBank.Site.Repositories;

public class HealthPostRepository : IHealthPostRepository
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://localhost:7226/api";

    public HealthPostRepository(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("API");
    }
    
    public async Task<ResultViewModel<List<HealthPostViewModel>>> GetAll()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/healthposts");
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return ResultViewModel<List<HealthPostViewModel>>.Error(error);
            }

            var healthPosts = await response.Content.ReadFromJsonAsync<List<HealthPostViewModel>>();
            return ResultViewModel<List<HealthPostViewModel>>.Success(healthPosts!);
        }
        catch (Exception e)
        {
            return ResultViewModel<List<HealthPostViewModel>>.Error(e.Message);
        }
    }
}