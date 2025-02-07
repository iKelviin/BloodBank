using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace BloodBank.Core.Interfaces.Services;

public class RabbitMqClientService : IBusService
{
    private readonly IModel _channel;
    const string EXCHANGE = "bloodbank";
    const string BLOOD_COLLECTED_QUEUE = "blood-collected";
    const string BLOOD_APPROVED_QUEUE = "blood-approved";
    const string ERROR_QUEUE = "blood-collected-error";
    
    public RabbitMqClientService()
    {
        var connectionFactory = new ConnectionFactory()
        {
            HostName = "localhost"
        };

        var connection = connectionFactory.CreateConnection("bloodbank-api-client-publisher");
        _channel = connection.CreateModel();
        _channel.ExchangeDeclare(EXCHANGE, ExchangeType.Topic, durable: true);
        _channel.QueueDeclare(BLOOD_COLLECTED_QUEUE, durable: true, exclusive: false, autoDelete: false);
        _channel.QueueBind(BLOOD_COLLECTED_QUEUE, EXCHANGE, routingKey: "blood-collected");
        _channel.QueueDeclare(ERROR_QUEUE, durable: true, exclusive: false, autoDelete: false);
        _channel.QueueDeclare(BLOOD_APPROVED_QUEUE, durable: true, exclusive: false, autoDelete: false);
        _channel.QueueBind(BLOOD_APPROVED_QUEUE, EXCHANGE, routingKey: "blood-approved");
    }
    
    public void Publish<T>(string routingKey, T message)
    {
        var json = JsonSerializer.Serialize(message);

        var byteArray = Encoding.UTF8.GetBytes(json);

        _channel.BasicPublish(EXCHANGE, routingKey, null, byteArray);
    }
}