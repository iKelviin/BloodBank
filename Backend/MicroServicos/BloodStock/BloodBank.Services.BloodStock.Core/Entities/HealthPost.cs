namespace BloodBank.Services.BloodStock.Core.Entities;

public class HealthPost
{
    protected HealthPost(){}
    public HealthPost(Guid id,string name, string address, string city)
    {
        Id = id;
        Name = name;
        Address = address;
        City = city;
    }

    public Guid Id { get; set; }
    public string Name { get; private set; } 
    public string Address { get; private set; }
    public string City { get; private set; } 
    
    public List<Donation> Donations { get; private set; }
}