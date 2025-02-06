using BloodBank.Core.Entities;

namespace BloodBank.Application.Models;

public class StockViewModel
{
    public StockViewModel(string blood, int quantityMl)
    {
        Blood = blood;
        QuantityML = quantityMl;
    }

    public string Blood { get; set; }
    public int QuantityML { get; set; }
    
    public static StockViewModel FromEntity(Stock entity) => new($"{entity.BloodType}{entity.RhFactor}", entity.QuantityML);
}