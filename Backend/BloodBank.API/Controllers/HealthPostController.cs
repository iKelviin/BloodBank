using BloodBank.Application.Queries.HealthPostQueries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodBank.API.Controllers;

[ApiController]
[Route("api/locations")]
public class HealthPostController : ControllerBase
{
    private readonly IMediator _mediator;

    public HealthPostController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllHealthPostQuery());
        if (!result.IsSuccess) return BadRequest(result.Message);
        return Ok(result.Data);
    }
}