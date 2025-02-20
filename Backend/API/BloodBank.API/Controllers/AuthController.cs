using BloodBank.Application.Commands.AuthCommands.LoginCommand;
using BloodBank.Application.Commands.AuthCommands.RegisterCommand;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodBank.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : Controller
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPut("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDonorCommand command)
    {
        var result = await _mediator.Send(command);
        
        if(!result.IsSuccess) return BadRequest(result.Message);
        
        return Ok(result.Data);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterDonorCommand command)
    {
        var result = await _mediator.Send(command);
        if(!result.IsSuccess) return BadRequest(result.Message);
        return Ok(result.Data);
    }
}