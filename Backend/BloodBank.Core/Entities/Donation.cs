namespace BloodBank.Core.Entities;

public class Donation : BaseEntity
{
    protected Donation() { }
    public Donation(Guid donorId, DateTime donationDate, int quantityMl) : base()
    {
        DonorId = donorId;
        DonationDate = donationDate;
        QuantityMl = quantityMl;
    }

    public Guid DonorId { get; private set; }
    public DateTime DonationDate { get; private set; }
    public int QuantityMl { get; private set; }
    
    public Donor Donor { get; private set; }

    public void Update(Guid donorId, DateTime donationDate, int quantityMl)
    {
        DonorId = donorId;
        DonationDate = donationDate;
        QuantityMl = quantityMl;
    }
}