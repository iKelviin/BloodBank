using BloodBank.Application.Commands.DonationCommands.InsertDonation;
using BloodBank.Application.Queries.DonationQueries.GetAll;
using BloodBank.Application.Queries.DonationQueries.GetByDonorId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodBank.API.Controllers;

[ApiController]
[Route("api/donations")]
public class DonationController : ControllerBase
{
    private readonly IMediator _mediator;

    public DonationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllDonationsQuery());
        if(!result.IsSuccess) return BadRequest(result.Message); 
        return Ok(result.Data);
    }
    
    [HttpGet("by-donor/{id}")]
    public async Task<IActionResult> GetByDonorId(Guid id)
    {
        var result = await _mediator.Send(new GetDonationsByDonorIdQuery(id));
        if(!result.IsSuccess) return BadRequest(result.Message); 
        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Post(InsertDonationCommand command)
    {
        var result = await _mediator.Send(command);
        if(!result.IsSuccess) return BadRequest(result.Message);
        return Ok(result.Data);
    }
}