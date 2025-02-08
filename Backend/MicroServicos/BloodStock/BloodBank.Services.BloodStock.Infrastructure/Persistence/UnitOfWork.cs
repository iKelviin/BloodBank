using BloodBank.Services.BloodStock.Core.Interfaces;
using BloodBank.Services.BloodStock.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace BloodBank.Services.BloodStock.Infrastructure.Persistence;


public class UnitOfWork : IUnitOfWork
{
    private readonly BloodBankDbContext _context;
    private IDbContextTransaction _transaction;

    public UnitOfWork(BloodBankDbContext context,IDonorRepository donorRepository, IDonationRepository donationRepository, IStockRepository stockRepository)
    {
        _context = context;
        Donors = donorRepository;
        Donations = donationRepository;
        Stocks = stockRepository;
    }
    public IDonorRepository Donors { get; }
    public IDonationRepository Donations { get; }
    public IStockRepository Stocks { get; }
    
    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        try
        {
            await _transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await _transaction.RollbackAsync();
            throw ex;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context.Dispose();
        }
    }
}