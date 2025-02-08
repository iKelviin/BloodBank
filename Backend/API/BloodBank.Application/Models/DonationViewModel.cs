using BloodBank.Core.Entities;
using BloodBank.Core.Enums;

namespace BloodBank.Application.Models;

public class DonationViewModel
{
    public DonationViewModel(Guid id, Guid donorId,Guid healthPostId,
        string name, DateTime donationDate, int quantityMl, string blood,
        string status, string location)
    {
        Id = id;
        DonorId = donorId;
        HealthPostId = healthPostId;
        Name = name;
        DonationDate = donationDate;
        QuantityMl = quantityMl;
        Blood = blood;
        Status = status;
        Location = location;
    }

    public Guid Id { get; set; }
    public Guid DonorId { get; set; }
    public Guid HealthPostId { get; set; }
    public string Name { get; set; }
    public DateTime DonationDate { get; set; }
    public int QuantityMl { get; set; }
    public string Blood { get; set; }
    public string Status { get; set; }
    public string Location { get; set; }

    public static DonationViewModel FromEntity(Donation donation)
    {
        var rhFactor = donation.Donor.RhFactor == RhFactorEnum.Positive ? "+" : "-";
        return new(donation.Id, donation.DonorId,donation.HealthPostId, donation.Donor.FullName, donation.DonationDate, donation.QuantityMl,
            $"{donation.Donor.BloodType.ToString()}{rhFactor}", donation.Status.ToString(), $"{donation.HealthPost.Name}: {donation.HealthPost.Address} - {donation.HealthPost.City}");
    }
}