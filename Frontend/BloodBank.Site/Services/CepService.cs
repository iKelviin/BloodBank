using System.Net.Http.Json;
using BloodBank.Site.Models;
using BloodBank.Site.Services.Interfaces;

namespace BloodBank.Site.Services;

public class CepService : ICepService
{
    public async Task<LocalAddress?> SearchByPostalCode(string postalCode)
    {
        var cep = postalCode.Replace(".", string.Empty).Replace("-", string.Empty);

        var http = new HttpClient();
        return await http.GetFromJsonAsync<LocalAddress>($"https://viacep.com.br/ws/{cep}/json/");
    }
}