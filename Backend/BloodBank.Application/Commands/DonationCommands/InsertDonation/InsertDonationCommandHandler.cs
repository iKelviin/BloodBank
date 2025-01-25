using BloodBank.Application.Models;
using BloodBank.Core.Entities;
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

            if (!(request.QuantityMl >= 420 && request.QuantityMl <= 470))
                return ResultViewModel<Guid>.Error("Quantity blood must be between 420Ml and 470Ml");

            // Inicia Transação com UnitOfWork
            await _unitOfWork.BeginTransactionAsync();
            
            // Adiciona a Doação
            var donation = request.ToEntity();
            await _unitOfWork.Donations.Add(donation);
            await _unitOfWork.CompleteAsync();
            
            // Adiciona sangue ao estoque
            var stock = await _unitOfWork.Stocks.GetByBloodType(donor.BloodType, donor.RhFactor);
            if (stock is null)
            {
                await _unitOfWork.Stocks.Add(new Stock(donor.BloodType, donor.RhFactor,request.QuantityMl));
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