using BuildingBlocks.Application.Dtos;
using Installations.Application.Commands;
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
    
    [HttpPost]
    public async Task<IActionResult> CreateInstallation(AddInstallationCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetInstallationById), new { installationId = result.Id }, result);
    }
    
    [HttpPut("{installationId:guid}")]
    public async Task<IActionResult> UpdateInstallation(Guid installationId, UpdateInstallationCommand command)
    {
        command.InstallationId = installationId;
        var updatedInstallation = await _mediator.Send(command);
        return Ok(updatedInstallation);
    }
    
    [HttpDelete("{installationId:guid}")]
    public async Task<IActionResult> DeleteInstallation(Guid installationId)
    {
        await _mediator.Send(new DeleteInstallationCommand { InstallationId = installationId });
        return NoContent();
    }
}