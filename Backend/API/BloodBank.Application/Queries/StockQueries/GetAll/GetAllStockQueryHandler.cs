using BloodBank.Application.Models;
using BloodBank.Core.Interfaces;
using MediatR;

namespace BloodBank.Application.Queries.StockQueries.GetAll;

public class GetAllStockQueryHandler : IRequestHandler<GetAllStockQuery, ResultViewModel<List<StockViewModel>>>
{
    private readonly IStockRepository _stockRepository;

    public GetAllStockQueryHandler(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    public async Task<ResultViewModel<List<StockViewModel>>> Handle(GetAllStockQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var stocks = await _stockRepository.GetAll();
            var model = stocks.Select(StockViewModel.FromEntity).ToList();
            return ResultViewModel<List<StockViewModel>>.Success(model);
        }
        catch (Exception e)
        {
            return ResultViewModel<List<StockViewModel>>.Error($"Error occured: {e.Message}");
        }
    }
}