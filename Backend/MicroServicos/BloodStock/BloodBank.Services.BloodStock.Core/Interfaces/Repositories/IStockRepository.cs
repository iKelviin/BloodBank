using BloodBank.Services.BloodStock.Core.Entities;

namespace BloodBank.Services.BloodStock.Core.Interfaces.Repositories;

public interface IStockRepository
{
    Task<List<Stock>> GetAll();
    Task<Stock> GetById(Guid id);
    Task<Stock> GetByBloodType(string bloodType, string rhFactor);
    Task<Guid> Add(Stock stock);
    Task Update(Stock stock);
}