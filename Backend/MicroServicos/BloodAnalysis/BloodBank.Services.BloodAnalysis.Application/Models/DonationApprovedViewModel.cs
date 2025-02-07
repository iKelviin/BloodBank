using BloodBank.Services.BloodAnalysis.Core.Entities;
using BloodBank.Services.BloodAnalysis.Core.Enums;

namespace BloodBank.Services.BloodAnalysis.Models;

public class DonationApprovedViewModel
{
    public DonationApprovedViewModel(Guid donationId, BloodTypeEnum bloodType, RhFactorEnum rhFactor, int quantityMl)
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

    public static DonationApprovedViewModel FromEntity(Donation donation)
    {
        return new(donation.Id,donation.Donor.BloodType,donation.Donor.RhFactor,donation.QuantityMl);
    }
}