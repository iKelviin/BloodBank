using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using BloodBank.Core.Enums;
using MediatR;

namespace BloodBank.Application.Commands.DonorCommands.InsertDonor;

public class InsertDonorCommand : IRequest<ResultViewModel<Guid>>
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime BirthDay { get; set; }
    public string Gender { get; set; }
    public Double Weight { get; set; }
    public string BloodType { get; set; }
    public string RhFactor { get; set; }

    // Address
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }

    public Donor ToEntity() => new(
        FullName,
        Email,
        BirthDay,
        Enum.Parse<GenderEnum>(Gender),
        Weight,
        BloodType,
        RhFactor,
        new Address(
            Street,
            City,
            State,
            ZipCode
        )
    );
}