using BloodBank.Core.Enums;

namespace BloodBank.Core.Entities;

public class Stock : BaseEntity
{
    protected Stock() { }
    public Stock(BloodTypeEnum bloodType, RhFactorEnum rhFactor, int quantityMl) : base()
    {
        BloodType = bloodType;
        RhFactor = rhFactor;
        QuantityML = quantityMl;
    }

    public BloodTypeEnum BloodType { get; private set; }
    public RhFactorEnum RhFactor { get; private set; }
    public int QuantityML { get; private set; }

    public void InsertBlood(int quantityMl)
    {
        QuantityML += quantityMl;
    }

    public void RemoveBlood(int quantityMl)
    {
        QuantityML -= quantityMl;
    }

    public void Update(BloodTypeEnum bloodType, RhFactorEnum rhFactor, int quantityMl)
    {
        BloodType = bloodType;
        RhFactor = rhFactor;
        QuantityML = quantityMl;
    }
}