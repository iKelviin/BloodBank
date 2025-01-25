using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using MediatR;

namespace BloodBank.Application.Commands.DonationCommands.InsertDonation;

public class InsertDonationCommand : IRequest<ResultViewModel<Guid>>
{
    public InsertDonationCommand(Guid donorId, int quantityMl)
    {
        DonorId = donorId;
        DonationDate = DateTime.Now;
        QuantityMl = quantityMl;
    }

    public Guid DonorId { get;  set; }
    public DateTime DonationDate { get; private set; }
    public int QuantityMl { get;  set; }
    
    public Donation ToEntity() => new(DonorId, DonationDate, QuantityMl);
}