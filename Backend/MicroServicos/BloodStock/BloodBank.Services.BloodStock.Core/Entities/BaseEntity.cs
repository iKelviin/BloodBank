namespace BloodBank.Services.BloodStock.Core.Entities;

public class BaseEntity
{
    public BaseEntity()
    {
        IsDeleted = false;
        CreatedAt = DateTime.Now;
    }

    public Guid Id { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    public void SetAsDeleted() => IsDeleted = true; 
}