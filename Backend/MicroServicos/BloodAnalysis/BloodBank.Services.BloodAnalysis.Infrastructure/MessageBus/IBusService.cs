namespace BloodBank.Services.BloodAnalysis.Infrastructure;

public interface IBusService
{
    void Publish<T>(string routingKey, T message);
}