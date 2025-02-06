using BloodBank.Application.Queries.StockQueries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodBank.API.Controllers;


[ApiController]
[Route("api/stocks")]
public class StockController : ControllerBase
{
    private readonly IMediator _mediator;

    public StockController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllStockQuery());
        if(!result.IsSuccess) return BadRequest(result.Message); 
        return Ok(result.Data);
    }
    
}