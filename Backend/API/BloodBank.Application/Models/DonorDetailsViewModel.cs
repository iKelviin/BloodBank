using BloodBank.Core.Entities;
using BloodBank.Core.Enums;

namespace BloodBank.Application.Models;

public class DonorDetailsViewModel
{
    public DonorDetailsViewModel(Guid id, string fullName, string email, DateTime birthDay,int age, string gender, double weight,
        string blood,string bloodType,string rhFactor, string street, string city, string state, string zipCode)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        BirthDay = birthDay;
        Age = age;
        Gender = gender;
        Weight = weight;
        Blood = blood;
        BloodType = bloodType;
        RhFactor = rhFactor;
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
    }

    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDay { get; private set; }
    public int Age { get; private set; }
    public string Gender { get; private set; }
    public Double Weight { get; private set; }
    public string Blood { get; private set; }
    public string BloodType { get; private set; }
    public string RhFactor { get; private set; }

    // Address
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string ZipCode { get; private set; }
    public string LastDonation { get; set; }

    public static DonorDetailsViewModel FromEntity(Donor donor)
    {
        var rhFactor = donor.RhFactor == RhFactorEnum.Positive ? "+" : "-";
        return new(donor.Id, donor.FullName, donor.Email, donor.BirthDay,donor.CalculateAge(donor.BirthDay), Enum.GetName(donor.Gender)!, donor.Weight, $"{donor.BloodType.ToString()}{rhFactor}",donor.BloodType.ToString(),donor.RhFactor.ToString(), donor.Address.Street, donor.Address.City, donor.Address.State, donor.Address.ZipCode);
    }
}