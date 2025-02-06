using BloodBank.Core.Enums;

namespace BloodBank.Core.Entities;

public class Donor : BaseEntity
{
    protected Donor() { }
    public Donor(string fullName, string email, DateTime birthDay, GenderEnum gender, double weight, BloodTypeEnum bloodType, RhFactorEnum rhFactor, Address address) : base()
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
    public BloodTypeEnum BloodType { get; private set; }
    public RhFactorEnum RhFactor { get; private set; }
    public Address Address { get; private set; }
    
    public List<Donation> Donations { get; private set; }
    
    public int CalculateAge(DateTime birthDate)
    {
        int age = DateTime.Now.Year - birthDate.Year;
        if (birthDate.Date > DateTime.Now.AddYears(-age))
        {
            age--;
        }
        return age;
    }

    public void Update(string fullName,string email,DateTime birthDay, GenderEnum gender, double weight, BloodTypeEnum bloodType, RhFactorEnum rhFactor, Address address)
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

    public bool CanDonate()
    {
        var age = CalculateAge(BirthDay);
        if(age < 16 && Weight < 50) return false;
        
        return true;
    }
}