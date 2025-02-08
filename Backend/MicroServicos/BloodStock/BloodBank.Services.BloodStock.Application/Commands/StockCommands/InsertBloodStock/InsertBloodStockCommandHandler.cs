using BloodBank.Services.BloodStock.Application.Models;
using BloodBank.Services.BloodStock.Core.Entities;
using BloodBank.Services.BloodStock.Core.Interfaces.Repositories;
using MediatR;

namespace BloodBank.Services.BloodStock.Application.Commands.StockCommands.InsertBloodStock;

public class InsertBloodStockCommandHandler : IRequestHandler<InsertBloodStockCommand, ResultViewModel>
{
    private readonly IStockRepository _repository;

    public InsertBloodStockCommandHandler(IStockRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel> Handle(InsertBloodStockCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var stock = await _repository.GetByBloodType(request.BloodType.ToString(), request.RhFactor.ToString());
            if (stock is null)
            {
                await _repository.Add(new Stock(request.BloodType.ToString(), request.RhFactor.ToString(),
                    request.QuantityMl));
            }
            else
            {
                stock.InsertBlood(request.QuantityMl);
                await _repository.Update(stock);
            }

            return ResultViewModel.Success();
        }
        catch (Exception e)
        {
            return ResultViewModel.Error($"Error occured: {e.Message}");
        }
    }
}