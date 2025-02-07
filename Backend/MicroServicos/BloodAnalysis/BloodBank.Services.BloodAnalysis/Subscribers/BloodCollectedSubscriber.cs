using System.Text;
using System.Text.Json;
using BloodBank.Services.BloodAnalysis.Commands.DonationCommands;
using BloodBank.Services.BloodAnalysis.Infrastructure;
using BloodBank.Services.BloodAnalysis.Models;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BloodBank.Services.BloodAnalysis.Subscribers;

public class BloodCollectedSubscriber : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private  IModel _channel;
    private  IConnection _connection;
    
    const string EXCHANGE = "bloodbank";
    const string BLOOD_COLLECTED_QUEUE = "blood-collected";
    const string BLOOD_APPROVED_QUEUE = "blood-approved";
    const string ERROR_QUEUE = "blood-collected-error";

    public BloodCollectedSubscriber(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    { 
        var factory = new ConnectionFactory { HostName = "localhost" };
        _connection = factory.CreateConnection("bloodbank-analysis-client-consumer");
        _channel = _connection.CreateModel();
        
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (sender, eventArgs) =>
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                var bus = scope.ServiceProvider.GetRequiredService<IBusService>();
                
                var contentArray = eventArgs.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(contentArray);
                var entity = JsonSerializer.Deserialize<BloodColletedViewModel>(contentString);

                Console.WriteLine($"Message received: {contentString}");

                var result = await mediator.Send(new SetDonationApprovedCommand(entity.DonationId));

                if (result.IsSuccess)
                {
                    var json = JsonSerializer.Serialize( result.Data);
                    var byteArray = Encoding.UTF8.GetBytes(json);
                    
                    // Publica na fila de BloodApproved
                    _channel.BasicPublish(
                        exchange: EXCHANGE,
                        routingKey: BLOOD_APPROVED_QUEUE,
                        basicProperties: null,
                        body: byteArray
                    );
                    _channel.BasicNack(eventArgs.DeliveryTag, false,false);
                }
                else
                {
                    // Publica na fila de erro
                    _channel.BasicPublish(
                        exchange: "",
                        routingKey: ERROR_QUEUE,
                        basicProperties: null,
                        body: contentArray
                    );
                    _channel.BasicNack(eventArgs.DeliveryTag, false, false);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                // Rejeita a mensagem e não a recoloca na fila
                _channel.BasicNack(eventArgs.DeliveryTag, false, true);
            }
        };
        
        _channel.BasicConsume(BLOOD_COLLECTED_QUEUE, false, consumer);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _channel.Close();
        _connection.Close();
        return Task.CompletedTask;
    }
}