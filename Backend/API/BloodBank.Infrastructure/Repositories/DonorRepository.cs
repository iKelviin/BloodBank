using BloodBank.Core.Entities;
using BloodBank.Core.Interfaces;
using BloodBank.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Infrastructure.Repositories;

public class DonorRepository : IDonorRepository
{
    private readonly BloodBankDbContext _context;

    public DonorRepository(BloodBankDbContext context)
    {
        _context = context;
    }

    public async Task<List<Donor>> GetAll()
    {
        var donors = await _context.Donors.Where(x=> !x.IsDeleted).ToListAsync();
        return donors;
    }

    public async Task<Donor> GetById(Guid id)
    {
        var donor = await _context.Donors.SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        return donor;
    }

    public async Task<bool> Exists(string email)
    {
        var exists = await _context.Donors.AnyAsync(x => x.Email == email && !x.IsDeleted);
        return exists;
    }

    public async Task<Guid> Add(Donor donor)
    {
        _context.Donors.Add(donor);
        await _context.SaveChangesAsync();
        return donor.Id;
    }

    public async Task Update(Donor donor)
    {
        _context.Donors.Update(donor);
        await _context.SaveChangesAsync();
    }

    public async Task<Donor?> GetDonorByEmailAndPassword(string email, string password)
    {
        return await _context.Donors.SingleOrDefaultAsync(x => x.Email == email && x.Password == password);
    }
}