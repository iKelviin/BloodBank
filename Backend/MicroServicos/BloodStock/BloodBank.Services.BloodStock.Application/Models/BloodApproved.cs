namespace BloodBank.Services.BloodStock.Application.Models;

public class BloodApproved
{
    public BloodApproved(Guid donationId, string bloodType, string rhFactor, int quantityMl)
    {
        DonationId = donationId;
        BloodType = bloodType;
        RhFactor = rhFactor;
        QuantityMl = quantityMl;
    }

    public Guid DonationId { get; set; }
    public string BloodType { get; set; }
    public string RhFactor { get; set; }
    public int QuantityMl { get; set; }
}