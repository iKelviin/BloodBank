using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BloodBank.Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BloodBank.Infrastructure.Security;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string ComputeHash(string password)
    {
        using (var hash = SHA256.Create())
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = hash.ComputeHash(passwordBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }

    public string GenerateToken(Donor donor)
    {
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
        );
        
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, donor.Email),
            new Claim(ClaimTypes.Role, donor.Role),
            new Claim(ClaimTypes.Name, donor.FullName)
        };
        
        var token = new JwtSecurityToken(issuer,audience,claims,null,DateTime.Now.AddHours(2),credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}