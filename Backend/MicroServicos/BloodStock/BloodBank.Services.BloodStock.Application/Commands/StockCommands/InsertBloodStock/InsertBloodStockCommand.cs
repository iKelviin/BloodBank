using BloodBank.Services.BloodStock.Application.Models;
using BloodBank.Services.BloodStock.Core.Enums;
using MediatR;

namespace BloodBank.Services.BloodStock.Application.Commands.StockCommands.InsertBloodStock;

public class InsertBloodStockCommand : IRequest<ResultViewModel>
{
    public InsertBloodStockCommand(BloodTypeEnum bloodType, RhFactorEnum rhFactor, int quantityMl)
    {
        BloodType = bloodType;
        RhFactor = rhFactor;
        QuantityMl = quantityMl;
    }

    public BloodTypeEnum BloodType { get; set; }
    public RhFactorEnum RhFactor { get; set; }
    public int QuantityMl { get; set; }

}