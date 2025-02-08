using BloodBank.Services.BloodStock.Core.Enums;

namespace BloodBank.Services.BloodStock.Core.Entities;

public class Donation : BaseEntity
{
    protected Donation() { }
    public Donation(Guid donorId, Guid healthPostId, DateTime donationDate, int quantityMl) : base()
    {
        DonorId = donorId;
        HealthPostId = healthPostId;
        DonationDate = donationDate;
        QuantityMl = quantityMl;
        Status = DonationStatus.Scheduled;
    }

    public Guid DonorId { get; private set; }
    public Guid HealthPostId { get; private set; }
    public DateTime DonationDate { get; private set; }
    public int QuantityMl { get; private set; }
    public DonationStatus Status { get; private set; }

    public Donor Donor { get; private set; }
    public HealthPost HealthPost { get; private set; }


    public void SetAsCollected()
    {
        if (Status == DonationStatus.Scheduled)
        {
            Status = DonationStatus.Collected;
        }
    }

    public void SetAsAnalyzed()
    {
        if (Status == DonationStatus.Collected)
        {
            Status = DonationStatus.Analyzed;
        }
    }

    public void SetAsApproved()
    {
        if (Status == DonationStatus.Analyzed)
        {
            Status = DonationStatus.Approved;
        }
    }

    public void SetAsUsed()
    {
        if (Status == DonationStatus.Approved)
        {
            Status = DonationStatus.Used;
        }
    }
}