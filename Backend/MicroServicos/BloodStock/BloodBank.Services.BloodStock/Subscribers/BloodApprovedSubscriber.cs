using System.Text;
using System.Text.Json;
using BloodBank.Services.BloodStock.Application.Commands.StockCommands.InsertBloodStock;
using BloodBank.Services.BloodStock.Application.Events;
using BloodBank.Services.BloodStock.Core.Enums;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BloodBank.Services.Core.BloodStock.Subscribers;

public class BloodApprovedSubscriber : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private IModel _channel;
    private IConnection _connection;

    const string EXCHANGE = "bloodbank";
    const string BLOOD_APPROVED_QUEUE = "blood-approved";
    const string ERROR_QUEUE = "blood-approved-error";

    public BloodApprovedSubscriber(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        _connection = factory.CreateConnection("bloodbank-stock-approved-consumer");
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
                var entity = JsonSerializer.Deserialize<BloodApprovedEvent>(contentString);

                Console.WriteLine($"Message received: {contentString}");

                var result =
                    await mediator.Send(new InsertBloodStockCommand(entity!.BloodType, entity.RhFactor,
                        entity.QuantityMl));

                if (result.IsSuccess)
                {
                    var rhFactor = entity.RhFactor == RhFactorEnum.Positive ? "+" : "-";
                    Console.WriteLine($"Blood inserted in stock: {entity.BloodType}{rhFactor}: {entity.QuantityMl}Ml");
                    _channel.BasicNack(eventArgs.DeliveryTag, false, false);
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
                _channel.BasicNack(eventArgs.DeliveryTag, false, false);
            }
        };

        _channel.BasicConsume(BLOOD_APPROVED_QUEUE, false, consumer);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _channel.Close();
        _connection.Close();
        return Task.CompletedTask;
    }
}