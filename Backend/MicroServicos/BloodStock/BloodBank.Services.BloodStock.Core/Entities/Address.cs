using BloodBank.Services.Core.BloodStockEntities;

namespace BloodBank.Services.Core.BloodStock.Entities;

public class Address
{
    public Address(string street, string city, string state, string zipCode)
    {
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
    }

    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string ZipCode { get; private set; }
    public Donor Donor { get; private set; }

    public void Update(Address newAddress)
    {
        Street = newAddress.Street;
        City = newAddress.City;
        State = newAddress.State;
        ZipCode = newAddress.ZipCode;
    }
}