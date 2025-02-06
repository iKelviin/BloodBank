using BloodBank.Core.Entities;

namespace BloodBank.Application.Models;

public class HealthPostViewModel
{
    public HealthPostViewModel(Guid id, string name, string address, string city)
    {
        Id = id;
        Name = name;
        Address = address;
        City = city;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Address { get; private set; }
    public string City { get; private set; }

    public static HealthPostViewModel FromEntity(HealthPost entity)
    {
        return new (entity.Id, entity.Name, entity.Address, entity.City);
    }
}