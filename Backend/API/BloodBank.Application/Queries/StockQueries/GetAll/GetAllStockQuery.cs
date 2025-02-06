using BloodBank.Application.Models;
using MediatR;

namespace BloodBank.Application.Queries.StockQueries.GetAll;

public class GetAllStockQuery : IRequest<ResultViewModel<List<StockViewModel>>>
{
    
}