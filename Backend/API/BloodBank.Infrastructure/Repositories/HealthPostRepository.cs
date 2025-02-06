using BloodBank.Core.Entities;
using BloodBank.Core.Interfaces;
using BloodBank.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Infrastructure.Repositories;

public class HealthPostRepository : IHealthPostRepository
{
    private readonly BloodBankDbContext _context;

    public HealthPostRepository(BloodBankDbContext context)
    {
        _context = context;
    }
    public async Task<List<HealthPost>> GetAll()
    {
        var locations = await _context.HealthPosts.ToListAsync();
        return locations;
    }
}