using BuildingBlocks.Application.Dtos;
using Contracts.Application.Commands.Contracts.AddContract;
using Contracts.Application.Commands.Contracts.UpdateContract;
using Contracts.Application.Dtos.Contracts;
using Contracts.Application.Dtos.PaymentMethods;
using Contracts.Application.Queries.Contracts;
using Contracts.Application.Queries.Contracts.GetActionsOfContract;
using Contracts.Application.Queries.Contracts.GetPaymentsOfContract;
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

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<ContractDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetContracts([FromQuery] GetContractsQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
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

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ContractDetailDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateContract(Guid id, ContractUpdateDto dto)
    {
        var contract = await _mediator.Send(new UpdateContractCommand(id, dto));
        return Ok(contract);
    }

    [HttpGet("customer/{customerId:guid}")]
    public async Task<IActionResult> GetContracts(Guid customerId, [FromQuery] ContractOfCustomerFilterDto dto)
    {
        var contracts = await _mediator.Send(new ContractsOfCustomerQuery(customerId, dto));
        return Ok(contracts);
    }
    
    [HttpGet("{id:guid}/payments")]
    [ProducesResponseType(typeof(PagedResult<PaymentMethodDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPaymentsOfContract(Guid id, [FromQuery] PaymentsOfContractFilterDto dto)
    {
        var payments = await _mediator.Send(new GetPaymentsOfContractQuery(id, dto));
        return Ok(payments);
    }
    
    [HttpGet("{id:guid}/actions")]
    [ProducesResponseType(typeof(PagedResult<ContractActionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActionsOfContract(Guid id, [FromQuery] ActionsOfContractFilterDto dto)
    {
        var actions = await _mediator.Send(new GetActionsOfContractQuery(id, dto));
        return Ok(actions);
    }
}