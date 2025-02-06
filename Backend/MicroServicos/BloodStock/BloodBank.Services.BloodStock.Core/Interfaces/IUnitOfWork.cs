namespace BloodBank.Services.Core.BloodStock.Interfaces;

public interface IUnitOfWork
{
    IDonorRepository Donors { get; }
    IDonationRepository Donations { get; }
    IStockRepository Stocks { get; }
    
    Task<int> CompleteAsync();
    
    Task BeginTransactionAsync();
    Task CommitAsync();
}