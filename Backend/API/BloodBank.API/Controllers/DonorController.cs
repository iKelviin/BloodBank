using BloodBank.Application.Commands.DonorCommands.DeleteDonor;
using BloodBank.Application.Commands.DonorCommands.InsertDonor;
using BloodBank.Application.Commands.DonorCommands.UpdateDonor;
using BloodBank.Application.Queries.DonorQueries.GetAllDonors;
using BloodBank.Application.Queries.DonorQueries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodBank.API.Controllers;

[ApiController]
[Route("api/donors")]
[Authorize]
public class DonorController : ControllerBase
{
    private readonly IMediator _mediator;

    public DonorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllDonorsQuery());
        if(!result.IsSuccess) return BadRequest(result.Message); 
        return Ok(result.Data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetDonorByIdQuery(id));
        if(!result.IsSuccess) return BadRequest(result.Message);
        return Ok(result.Data);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post(InsertDonorCommand command)
    {
        var result = await _mediator.Send(command);
        if(!result.IsSuccess) return BadRequest(result.Message);
        return Ok(result.Data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, UpdateDonorCommand command)
    {
        var result = await _mediator.Send(command);
        if(!result.IsSuccess) return BadRequest(result.Message);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteDonorCommand(id));
        if(!result.IsSuccess) return BadRequest(result.Message);
        return NoContent();
    }
    
    [HttpPut("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var result = await mediator.Send(command);
        
        if(!result.IsSuccess) return BadRequest(result.Message);
        
        return Ok(result.Data);
    }
}