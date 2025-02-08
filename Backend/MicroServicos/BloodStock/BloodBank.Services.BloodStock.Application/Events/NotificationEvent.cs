namespace BloodBank.Services.BloodStock.Application.Events;

public class NotificationEvent
{
    public List<Guid> DonorIds { get; set; }
}