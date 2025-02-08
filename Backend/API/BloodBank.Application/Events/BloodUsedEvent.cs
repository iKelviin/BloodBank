using BloodBank.Core.Enums;

namespace BloodBank.Application.Events;

public class BloodUsedEvent
{
    public BloodTypeEnum BloodType { get; set; }
    public RhFactorEnum RhFactor { get; set; }
    public int QuantityMl { get; set; }
}