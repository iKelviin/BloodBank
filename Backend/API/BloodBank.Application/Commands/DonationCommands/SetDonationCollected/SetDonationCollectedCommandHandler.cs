using BloodBank.Application.Models;
using BloodBank.Core.Enums;
using BloodBank.Core.Interfaces;
using BloodBank.Core.Interfaces.Services;
using MediatR;

namespace BloodBank.Application.Commands.DonationCommands.SetDonationCollected;

public class SetDonationCollectedCommandHandler : IRequestHandler<SetDonationCollectedCommand, ResultViewModel>
{
    private readonly IDonationRepository _repository;
    private readonly IBusService _bus;
    private const string ROUTING_KEY = "blood-collected";

    public SetDonationCollectedCommandHandler(IDonationRepository repository, IBusService bus)
    {
        _repository = repository;
        _bus = bus;
    }

    public async Task<ResultViewModel> Handle(SetDonationCollectedCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var donation = await _repository.GetById(request.Id);
            if (donation == null) return ResultViewModel.Error("Donation not found");
            if(donation.Status != DonationStatus.Scheduled) return ResultViewModel.Error("Donation is not scheduled");

            donation.SetAsCollected();
            await _repository.Update(donation);

            // Publica evento BloodApproved para RabbitMQ
            var @event = new BloodCollectedPublishModel(donation.Id);
            _bus.Publish(ROUTING_KEY, @event);
            
            return ResultViewModel.Success();
        }
        catch (Exception e)
        {
            return ResultViewModel.Error($"Error occured: {e.Message}");
        }
    }
}