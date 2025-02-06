using System.Text;
using System.Text.Json;
using BloodStockService.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BloodStockService.Subscribers;

public class BloodApprovedSubscriber : IHostedService
{
    private readonly IModel _channel;
    const string EXCHANGE = "bloodbank";
    const string BLOOD_APPROVED_QUEUE = "blood-approved";

    public BloodApprovedSubscriber()
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        var connection = factory.CreateConnection("bloodbank-stock-client-consumer");
        _channel = connection.CreateModel();
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (sender, eventArgs) =>
        {
            var contentArray = eventArgs.Body.ToArray();
            var contentString = Encoding.UTF8.GetString(contentArray);
            var @event = JsonSerializer.Deserialize<BloodApproved>(contentString);
            
            Console.WriteLine($"Message received: {contentString}");

            _channel.BasicAck(eventArgs.DeliveryTag, false);
        };
        
        _channel.BasicConsume(BLOOD_APPROVED_QUEUE, false, consumer);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}