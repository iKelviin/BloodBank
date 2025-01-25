using BloodBank.Core.Entities;
using BloodBank.Core.Interfaces;
using BloodBank.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Infrastructure.Repositories;

public class StockRepository : IStockRepository
{
    private readonly BloodBankDbContext _context;

    public StockRepository(BloodBankDbContext context)
    {
        _context = context;
    }

    public async Task<List<Stock>> GetAll()
    {
        var stocks = await _context.Stocks
            .Where(x=> !x.IsDeleted)
            .ToListAsync();
        return stocks;
    }

    public async Task<Stock> GetById(Guid id)
    {
        var stock = await _context.Stocks.SingleOrDefaultAsync(x => x.Id == id);
        return stock;
    }
    
    public async Task<Stock> GetByBloodType(string bloodType, string rhFactor)
    {
        var stock = await _context.Stocks.SingleOrDefaultAsync(x => x.BloodType == bloodType && x.RhFactor == rhFactor);
        return stock;
    }

    public async Task<Guid> Add(Stock stock)
    {
        _context.Stocks.Add(stock);
        await _context.SaveChangesAsync();  
        return stock.Id;
    }

    public async Task Update(Stock stock)
    {
        _context.Stocks.Update(stock);
        await _context.SaveChangesAsync();
    }
}