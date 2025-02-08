using BloodBank.Core.Entities;
using BloodBank.Core.Enums;

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
    
    public static StockViewModel FromEntity(Stock entity)
    {
        var rhFactor = entity.RhFactor == RhFactorEnum.Positive.ToString() ? "+" : "-";
        return new StockViewModel($"{entity.BloodType}{rhFactor}", entity.QuantityML);
    }
}