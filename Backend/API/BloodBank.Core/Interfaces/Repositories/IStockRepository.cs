using BloodBank.Core.Entities;
using BloodBank.Core.Enums;

namespace BloodBank.Core.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetAll();
    Task<Stock> GetById(Guid id);
    Task<Stock> GetByBloodType(string bloodType, string rhFactor);
    Task<Guid> Add(Stock stock);
    Task Update(Stock stock);
}