using BloodBank.Site.Models;

namespace BloodBank.Site.Services.Interfaces;

public interface ICepService
{
    Task<LocalAddress?> SearchByPostalCode(string postalCode);
}