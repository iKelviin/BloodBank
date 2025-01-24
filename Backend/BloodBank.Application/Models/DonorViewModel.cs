using BloodBank.Core.Entities;

namespace BloodBank.Application.Models;

public class DonorViewModel
{
    public DonorViewModel(Guid id, string fullName, string email, int age, string gender, string blood)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        Age = age;
        Gender = gender;
        Blood = blood;
    }

    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public int Age { get; private set; }
    public string Gender { get; private set; }
    public string Blood { get; private set; }

    public static DonorViewModel FromEntity(Donor donor)
    {
        return new(donor.Id, donor.FullName, donor.Email, donor.CalculateAge(donor.BirthDay), Enum.GetName(donor.Gender)!, $"{donor.BloodType}{donor.RhFactor}");
    }

}