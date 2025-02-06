using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using BloodBank.Core.Enums;
using MediatR;

namespace BloodBank.Application.Commands.DonorCommands.InsertDonor;

public class InsertDonorCommand : IRequest<ResultViewModel<Guid>>
{
    public InsertDonorCommand(string fullName, string email, DateTime birthDay, string gender, double weight, string bloodType, string rhFactor, string street, string city, string state, string zipCode)
    {
        FullName = fullName;
        Email = email;
        BirthDay = birthDay;
        Gender = gender;
        Weight = weight;
        BloodType = bloodType;
        RhFactor = rhFactor;
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
    }

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
        Enum.Parse<BloodTypeEnum>(BloodType),
        Enum.Parse<RhFactorEnum>(RhFactor),
        new Address(
            Street,
            City,
            State,
            ZipCode
        )
    );
}