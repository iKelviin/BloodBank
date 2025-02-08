using BloodBank.Services.BloodStock.Core.Enums;

namespace BloodBank.Services.BloodStock.Application.Events;

public class BloodApprovedEvent
{
    public BloodApprovedEvent(Guid donationId, BloodTypeEnum bloodType, RhFactorEnum rhFactor, int quantityMl)
    {
        DonationId = donationId;
        BloodType = bloodType;
        RhFactor = rhFactor;
        QuantityMl = quantityMl;
    }

    public Guid DonationId { get; set; }
    public BloodTypeEnum BloodType { get; set; }
    public RhFactorEnum RhFactor { get; set; }
    public int QuantityMl { get; set; }
}