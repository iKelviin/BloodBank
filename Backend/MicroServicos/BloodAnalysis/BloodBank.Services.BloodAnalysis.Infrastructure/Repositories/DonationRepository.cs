using BloodBank.Services.BloodAnalysis.Core.Entities;
using BloodBank.Services.BloodAnalysis.Core.Interfaces;
using BloodBank.Services.BloodAnalysis.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Services.BloodAnalysis.Infrastructure.Repositories;

public class DonationRepository : IDonationRepository
{
    private readonly BloodBankDbContext _context;

    public DonationRepository(BloodBankDbContext context)
    {
        _context = context;
    }

    public async Task<List<Donation>> GetAll()
    {
        var donations = await _context.Donations
            .Include(x=> x.Donor)
            .Where(x=> !x.IsDeleted)
            .ToListAsync();
        return donations;
    }

    public async Task<List<Donation>> GetByDonorId(Guid donorId)
    {
        var donations = await _context.Donations
            .Include(x=> x.Donor)
            .Where(x=> x.DonorId == donorId && !x.IsDeleted)
            .ToListAsync();
        return donations;
    }
    
    public async Task<Donation> GetLastByDonorId(Guid donorId)
    {
        var donation = await _context.Donations
            .Include(x => x.Donor)
            .Where(x => x.DonorId == donorId && !x.IsDeleted)
            .OrderByDescending(x => x.DonationDate)
            .FirstOrDefaultAsync();
        return donation;
    }

    public async Task<Donation> GetById(Guid id)
    {
        var donation = await _context.Donations.Include(x=> x.Donor).SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        return donation;
    }

    public async Task<Guid> Add(Donation donation)
    {
        await _context.Donations.AddAsync(donation);
        await _context.SaveChangesAsync();
        return donation.Id;
    }

    public async Task Update(Donation donation)
    {
        _context.Donations.Update(donation);
        await _context.SaveChangesAsync();  
    }
}