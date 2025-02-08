using System.Text;
using System.Text.Json;
using BloodBank.Services.BloodStock.Application.Commands.StockCommands.UseBloodStock;
using BloodBank.Services.BloodStock.Application.Events;
using BloodBank.Services.BloodStock.Core.Enums;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BloodBank.Services.Core.BloodStock.Subscribers;

public class BloodUsedSubscriber : IHostedService
{
     private readonly IServiceProvider _serviceProvider;
    private IModel _channel;
    private IConnection _connection;
    
    const string BLOOD_USED_QUEUE = "blood-used";
    const string ERROR_QUEUE = "blood-used-error";

    public BloodUsedSubscriber(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        _connection = factory.CreateConnection("bloodbank-stock-used-consumer");
        _channel = _connection.CreateModel();

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (sender, eventArgs) =>
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                var contentArray = eventArgs.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(contentArray);
                var entity = JsonSerializer.Deserialize<BloodUsedEvent>(contentString);

                Console.WriteLine($"Message received: {contentString}");

                var command = new UseBloodCommand(entity!.BloodType, entity.RhFactor, entity.QuantityMl);
                var result = await mediator.Send(command);
                if (result.IsSuccess)
                {
                    var rh = entity.RhFactor == RhFactorEnum.Positive ? "+" : "-";
                    Console.WriteLine($"[{DateTime.Now}] Blood used: {entity.BloodType}{rh} {entity.QuantityMl}ml");
                    _channel.BasicNack(eventArgs.DeliveryTag, false, false);
                }
                else
                {
                    var rh = entity.RhFactor == RhFactorEnum.Positive ? "+" : "-";
                    Console.WriteLine($"[{DateTime.Now}] {result.Message}: {entity.BloodType}{rh} {entity.QuantityMl}ml");
                    
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
                _channel.BasicNack(eventArgs.DeliveryTag, false, false);
            }
        };

        _channel.BasicConsume(BLOOD_USED_QUEUE, false, consumer);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _channel.Close();
        _connection.Close();
        return Task.CompletedTask;
    }
}