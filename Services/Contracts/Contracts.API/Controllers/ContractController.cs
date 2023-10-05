using Contracts.Application.Commands.Contracts.AddContract;
using Contracts.Application.Commands.Contracts.ConvertContractFromOrder;
using Contracts.Application.Dtos.Contracts;
using Contracts.Application.Queries;
using Contracts.Application.Queries.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
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

    [HttpPost]
    [ProducesResponseType(typeof(ContractDetailDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateContract(ContractCreateDto dto)
    {
        var contract = await _mediator.Send(new AddContractCommand(dto));
        return Ok(contract);
    }

    [HttpGet]
    public async Task<IActionResult> GetContracts()
    {
        var result = await _mediator.Send(new GetContractsQuery());
        return Ok(result);
    }

    [HttpGet("customer/{customerId:guid}")]
    public async Task<IActionResult> GetContracts(Guid customerId, [FromQuery]ContractOfCustomerFilterDto contractOfCustomerFilterDto)
    {
        var contracts = await _mediator.Send(new ContractsOfCustomerQuery(customerId, contractOfCustomerFilterDto));
        return Ok(contracts);
    }
}