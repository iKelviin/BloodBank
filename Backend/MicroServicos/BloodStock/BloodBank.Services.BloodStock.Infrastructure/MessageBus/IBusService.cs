namespace BloodBank.Services.BloodStock.Infrastructure;

public interface IBusService
{
    void Publish<T>(string routingKey, T message);
}