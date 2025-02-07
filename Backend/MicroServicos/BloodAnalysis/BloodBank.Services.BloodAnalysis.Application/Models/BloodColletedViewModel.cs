namespace BloodBank.Services.BloodAnalysis.Models;

public class BloodColletedViewModel
{
    public BloodColletedViewModel(Guid donationId)
    {
        DonationId = donationId;
    }

    public Guid DonationId { get; set; }
}