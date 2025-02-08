using BloodBank.Services.BloodStock.Core.Enums;

namespace BloodBank.Services.BloodStock.Application.Events;

public class BloodUsedEvent
{
    public BloodTypeEnum BloodType { get; set; }
    public RhFactorEnum RhFactor { get; set; }
    public int QuantityMl { get; set; }
}