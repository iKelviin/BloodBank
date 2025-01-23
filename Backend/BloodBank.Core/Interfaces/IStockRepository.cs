using BloodBank.Core.Entities;

namespace BloodBank.Core.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetAll();
    Task<Stock> GetById(Guid id);
    Task<Guid> Add(Stock stock);
    Task Update(Stock stock);
}