namespace BloodBank.Core.Interfaces.Services;

public interface IBusService
{
    void Publish<T>(string routingKey, T message);
}