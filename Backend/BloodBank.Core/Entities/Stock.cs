namespace BloodBank.Core.Entities;

public class Stock : BaseEntity
{
    protected Stock() { }
    public Stock(string bloodType, string rhFactor, int quantityMl) : base()
    {
        BloodType = bloodType;
        RhFactor = rhFactor;
        QuantityML = quantityMl;
    }

    public string BloodType { get; private set; }
    public string RhFactor { get; private set; }
    public int QuantityML { get; private set; }

    public void InsertBlood(int quantityMl)
    {
        QuantityML += quantityMl;
    }

    public void RemoveBlood(int quantityMl)
    {
        QuantityML -= quantityMl;
    }

    public void Update(string bloodType, string rhFactor, int quantityMl)
    {
        BloodType = bloodType;
        RhFactor = rhFactor;
        QuantityML = quantityMl;
    }
}