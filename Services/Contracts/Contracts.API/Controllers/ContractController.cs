using Contracts.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Contracts.API.Controllers;

[Route("api/contracts")]
[ApiController]
public class ContractController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public ContractController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetContracts(Guid id)
    {
        var result = await _mediator.Send(new GetContractByIdQuery(id));
        return Ok(result);
    }
}