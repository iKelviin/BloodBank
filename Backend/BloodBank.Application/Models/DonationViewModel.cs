using BloodBank.Core.Entities;

namespace BloodBank.Application.Models;

public class DonationViewModel
{
    public DonationViewModel(Guid id, Guid donorId, string name, DateTime donationDate, int quantityMl, string blood)
    {
        Id = id;
        DonorId = donorId;
        Name = name;
        DonationDate = donationDate;
        QuantityMl = quantityMl;
        Blood = blood;
    }

    public Guid Id { get; set; }
    public Guid DonorId { get; set; }
    public string Name { get; set; }
    public DateTime DonationDate { get; set; }
    public int QuantityMl { get; set; }
    public string Blood { get; set; }

    public static DonationViewModel FromEntity(Donation donation) => new (donation.Id, donation.DonorId,
        donation.Donor.FullName, donation.DonationDate, donation.QuantityMl,
        $"{donation.Donor.BloodType}{donation.Donor.RhFactor}");
}