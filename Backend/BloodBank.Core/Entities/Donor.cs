using BloodBank.Core.Enums;

namespace BloodBank.Core.Entities;

public class Donor : BaseEntity
{
    protected Donor() { }
    public Donor(string fullName, string email, DateTime birthDay, GenderEnum gender, double weight, string bloodType, string rhFactor, Address address) : base()
    {
        FullName = fullName;
        Email = email;
        BirthDay = birthDay;
        Gender = gender;
        Weight = weight;
        BloodType = bloodType;
        RhFactor = rhFactor;
        Address = address;
    }

    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDay { get; private set; }
    public GenderEnum Gender { get; private set; }
    public Double Weight { get; private set; }
    public string BloodType { get; private set; }
    public string RhFactor { get; private set; }
    public Address Address { get; private set; }
    
    public List<Donation> Donations { get; private set; }

    public void Update(string fullName,string email,DateTime birthDay, GenderEnum gender, double weight, string bloodType, string rhFactor, Address address)
    {
        FullName = fullName;
        Email = email;
        BirthDay = birthDay;
        Gender = gender;
        Weight = weight;
        BloodType = bloodType;
        RhFactor = rhFactor;
        Address = address;
    }
}