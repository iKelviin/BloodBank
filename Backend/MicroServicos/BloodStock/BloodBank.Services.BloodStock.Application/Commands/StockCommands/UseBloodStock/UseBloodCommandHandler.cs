using BloodBank.Services.BloodStock.Application.Events;
using BloodBank.Services.BloodStock.Application.Models;
using BloodBank.Services.BloodStock.Core.Entities;
using BloodBank.Services.BloodStock.Core.Interfaces;
using BloodBank.Services.BloodStock.Core.Interfaces.Repositories;
using BloodBank.Services.BloodStock.Infrastructure;
using MediatR;

namespace BloodBank.Services.BloodStock.Application.Commands.StockCommands.UseBloodStock;

public class UseBloodCommandHandler : IRequestHandler<UseBloodCommand, ResultViewModel>
{
    
    private readonly IUnitOfWork _repository;
    private readonly IBusService _bus;
    private const string ROUTING_KEY = "blood-used-notification";

    public UseBloodCommandHandler( IBusService bus, IUnitOfWork unitOfWork)
    {
        _repository = unitOfWork;
        _bus = bus;
    }

    public async Task<ResultViewModel> Handle(UseBloodCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _repository.BeginTransactionAsync();
            
            var stock = await _repository.Stocks.GetByBloodType(request.BloodType.ToString(), request.RhFactor.ToString());
            if (stock is null || stock.QuantityML < request.QuantityMl)
                return ResultViewModel.Error("Stock is empty or insufficient.");

            stock.RemoveBlood(request.QuantityMl);
            await _repository.Stocks.Update(stock);
            await _repository.CompleteAsync();
            
            var donations = await _repository.Donations.GetAllByBloodType(request.BloodType.ToString(), request.RhFactor.ToString());
            
            var usedDonations = new List<Donation>();
            var remainingQuantity = request.QuantityMl;

            foreach (var donation in donations)
            {
                if (remainingQuantity <= 0) break;

                usedDonations.Add(donation);
                remainingQuantity -= donation.QuantityMl;
            }

            // Marca as doações como usadas
            foreach (var donation in usedDonations)
            {
                donation.SetAsUsed();
                await _repository.Donations.Update(donation);
                await _repository.CompleteAsync();
            }

            await _repository.CommitAsync();
            
            // Publica mensagem para notificar os doadores que o sangue foi usado.
            var donorIds = usedDonations.Select(d => d.DonorId).ToList();
            var @event = new NotificationEvent
            {
                DonorIds = donorIds
            };

            _bus.Publish(ROUTING_KEY,@event);

            return ResultViewModel.Success();
        }
        catch (Exception e)
        {
            return ResultViewModel.Error($"Error occured: {e.Message}");
        }
    }
}