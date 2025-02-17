using BloodBank.Core.Entities;

namespace BloodBank.Infrastructure.Security;

public interface IAuthService
{
    string ComputeHash(string password);
    string GenerateToken(Donor donor);
}