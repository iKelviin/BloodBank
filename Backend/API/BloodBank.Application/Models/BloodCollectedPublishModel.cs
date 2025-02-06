namespace BloodBank.Application.Models;

public class BloodCollectedPublishModel
{
    public BloodCollectedPublishModel(Guid donationId)
    {
        DonationId = donationId;
    }

    public Guid DonationId { get; set; }
}