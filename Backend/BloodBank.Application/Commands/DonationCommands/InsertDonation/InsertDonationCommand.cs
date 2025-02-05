using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using MediatR;

namespace BloodBank.Application.Commands.DonationCommands.InsertDonation;

public class InsertDonationCommand : IRequest<ResultViewModel<Guid>>
{
    public InsertDonationCommand(Guid donorId, Guid healthPostId, DateTime donationDate, int quantityMl)
    {
        DonorId = donorId;
        HealthPostId = healthPostId;
        DonationDate = donationDate;
        QuantityMl = quantityMl;
    }


    public Guid DonorId { get; set; }
    public Guid HealthPostId { get; set; }
    public DateTime DonationDate { get; set; }
    public int QuantityMl { get; set; }

    public Donation ToEntity() => new(DonorId, HealthPostId, DonationDate, QuantityMl);
}