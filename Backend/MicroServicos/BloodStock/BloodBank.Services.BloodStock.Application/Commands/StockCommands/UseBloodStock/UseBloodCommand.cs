using BloodBank.Services.BloodStock.Application.Models;
using BloodBank.Services.BloodStock.Core.Enums;
using MediatR;

namespace BloodBank.Services.BloodStock.Application.Commands.StockCommands.UseBloodStock;

public class UseBloodCommand : IRequest<ResultViewModel>
{
    public BloodTypeEnum BloodType { get; set; }
    public RhFactorEnum RhFactor { get; set; }
    public int QuantityMl { get; set; }

    public UseBloodCommand(BloodTypeEnum bloodType, RhFactorEnum rhFactor, int quantityMl)
    {
        BloodType = bloodType;
        RhFactor = rhFactor;
        QuantityMl = quantityMl;
    }
}