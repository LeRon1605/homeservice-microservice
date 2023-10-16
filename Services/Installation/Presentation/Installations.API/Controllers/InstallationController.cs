using BuildingBlocks.Application.Dtos;
using Installations.Application.Dtos;
using Installations.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Installations.API.Controllers;

[ApiController]
[Route("api/installations")]
public class InstallationController : ControllerBase
{
    private readonly IMediator _mediator;

    public InstallationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<InstallationDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetInstallations([FromQuery] GetAllInstallationsQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("contracts/{contractId:guid}")]
    public async Task<IActionResult> GetInstallationsByContractId(Guid contractId, [FromQuery] GetInstallationsByContractIdQuery query)
    {
        query.ContractId = contractId;
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("{installationId:guid}")]
    public async Task<IActionResult> GetInstallationById(Guid installationId)
    {
        var result = await _mediator.Send(new GetInstallationByIdQuery(installationId));
        return Ok(result);
    }
}