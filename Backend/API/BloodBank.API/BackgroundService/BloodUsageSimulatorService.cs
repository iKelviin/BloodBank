using BloodBank.Application.Events;
using BloodBank.Core.Enums;
using BloodBank.Core.Interfaces.Services;
using MassTransit;
using MediatR;
using RabbitMQ.Client;

namespace BloodBank.API.BackgroundService;

public class BloodUsageSimulatorService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private  IModel _channel;
    private  IConnection _connection;
    private Timer _timer;
    private readonly Random _random = new();
    private const string ROUTING_KEY = "blood-used";

    public BloodUsageSimulatorService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    { 
        _timer = new Timer(SimulateBloodUsage, null, TimeSpan.Zero, TimeSpan.FromMinutes(2));
        return Task.CompletedTask;
    }

    private async void SimulateBloodUsage(object state)
    {
        try
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                var bus = scope.ServiceProvider.GetRequiredService<IBusService>();

                // Escolhe um tipo de sangue e fator Rh aleatório
                var bloodType = (BloodTypeEnum)_random.Next(0, 4); // 0 a 3 (A, B, AB, O)
                var rhFactor = (RhFactorEnum)_random.Next(0, 2); // 0 a 1 (Negative, Positive)
                var quantityMl = _random.Next(400, 470); // Quantidade aleatória entre 420ml e 470ml
                
                // Publica mensagem para fila de Sangue usado que será consumido pelo serviço de BloodStock.
                var @event = new BloodUsedEvent
                {
                    BloodType = bloodType,
                    RhFactor = rhFactor,
                    QuantityMl = quantityMl
                };

                bus.Publish(ROUTING_KEY,@event);
               
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error occured: {e.Message}");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }
    
    public void Dispose()
    {
        _timer?.Dispose();
    }
}