using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace BloodBank.Services.BloodStock.Infrastructure;

public class RabbitMqClientService : IBusService
{
    private readonly IModel _channel;
    const string EXCHANGE = "bloodbank";
    
    public RabbitMqClientService()
    {
        var connectionFactory = new ConnectionFactory()
        {
            HostName = "localhost"
        };

        var connection = connectionFactory.CreateConnection("bloodbank-service-analysis-publisher");
        _channel = connection.CreateModel();
    }
    
    public void Publish<T>(string routingKey, T message)
    {
        var json = JsonSerializer.Serialize(message);

        var byteArray = Encoding.UTF8.GetBytes(json);

        _channel.BasicPublish(EXCHANGE, routingKey, null, byteArray);
    }
}