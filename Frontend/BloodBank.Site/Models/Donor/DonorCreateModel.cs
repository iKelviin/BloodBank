namespace BloodBank.Site.Models;

public class DonorCreateModel
{
    public DonorCreateModel(string fullName, string email, DateTime birthDay, string gender, double weight, string bloodType, string rhFactor, string street, string city, string state, string zipCode)
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
}