using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using BloodBank.Core.Enums;
using BloodBank.Core.Interfaces;
using MediatR;

namespace BloodBank.Application.Commands.DonationCommands.InsertDonation;

public class InsertDonationCommandHandler : IRequestHandler<InsertDonationCommand, ResultViewModel<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;

    public InsertDonationCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultViewModel<Guid>> Handle(InsertDonationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            
            // Verifica o doador
            var donor = await _unitOfWork.Donors.GetById(request.DonorId);
            if (donor is null) return ResultViewModel<Guid>.Error("Donor not found");

            if (!donor.CanDonate()) return ResultViewModel<Guid>.Error("Donor can't donate");
            
            // Verifica ultima doação feita
            var lastDonationByDonor = await _unitOfWork.Donations.GetLastByDonorId(request.DonorId);
            if (lastDonationByDonor != null)
            {
                var daysSinceLastDonation = (DateTime.Now - lastDonationByDonor.DonationDate).Days;

                // Homens só podem doar a cada 60 dias.
                if (donor.Gender == GenderEnum.Male && daysSinceLastDonation < 60)
                {
                    return ResultViewModel<Guid>.Error("Men can only donate blood every 60 days.");
                }
                // Mulheres só podem doar a cada 90 dias.
                else if (donor.Gender == GenderEnum.Female && daysSinceLastDonation < 90)
                {
                    return ResultViewModel<Guid>.Error("Women can only donate blood every 90 days.");
                }
            }
            // Inicia Transação com UnitOfWork
            await _unitOfWork.BeginTransactionAsync();
            
            // Adiciona a Doação
            var donation = request.ToEntity();
            await _unitOfWork.Donations.Add(donation);
            await _unitOfWork.CompleteAsync();
            
            // Adiciona sangue ao estoque
            var stock = await _unitOfWork.Stocks.GetByBloodType(Enum.Parse<BloodTypeEnum>(donor.BloodType),Enum.Parse<RhFactorEnum>( donor.RhFactor));
            if (stock is null)
            {
                await _unitOfWork.Stocks.Add(new Stock(Enum.Parse<BloodTypeEnum>(donor.BloodType),Enum.Parse<RhFactorEnum>( donor.RhFactor),request.QuantityMl));
            }
            else
            {
                stock.InsertBlood(request.QuantityMl);
                await _unitOfWork.Stocks.Update(stock);
            }
            await _unitOfWork.CompleteAsync();
            
            // Commita as transações do UnitOfWork
            await _unitOfWork.CommitAsync();
            
            return ResultViewModel<Guid>.Success(donation.Id);
        }
        catch (Exception e)
        {
            return ResultViewModel<Guid>.Error($"Error occured: {e.Message}");
        }
    }
}