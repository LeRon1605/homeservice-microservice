using BuildingBlocks.Application.Dtos;
using Contracts.Application.Commands.Contracts.AddActionToContract;
using Contracts.Application.Commands.Contracts.AddContract;
using Contracts.Application.Commands.Contracts.AddPaymentToContract;
using Contracts.Application.Commands.Contracts.DeleteActionFromContract;
using Contracts.Application.Commands.Contracts.DeletePaymentFromContract;
using Contracts.Application.Commands.Contracts.UpdateContract;
using Contracts.Application.Commands.Contracts.UpdateContractAction;
using Contracts.Application.Commands.Contracts.UpdateContractPayment;
using Contracts.Application.Dtos.Contracts;
using Contracts.Application.Dtos.Contracts.ContractCreate;
using Contracts.Application.Dtos.Contracts.ContractUpdate;
using Contracts.Application.Dtos.PaymentMethods;
using Contracts.Application.Queries.Contracts;
using Contracts.Application.Queries.Contracts.GetActionsOfContract;
using Contracts.Application.Queries.Contracts.GetContractById;
using Contracts.Application.Queries.Contracts.GetContractsOfCustomer;
using Contracts.Application.Queries.Contracts.GetContractsQuery;
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
    
    [HttpPost("{id:guid}/payments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddPaymentOfContract(Guid id, ContractPaymentCreateDto dto)
    {
        await _mediator.Send(new AddPaymentToContractCommand(id, dto));
        return NoContent();
    }
    
    [HttpPut("{id:guid}/payments/{paymentId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdatePaymentOfContract(Guid id, Guid paymentId, ContractPaymentUpdateDto dto)
    {
        await _mediator.Send(new UpdateContractPaymentCommand(id, paymentId, dto));
        return NoContent();
    }
    
    [HttpDelete("{id:guid}/payments/{paymentId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeletePaymentOfContract(Guid id, Guid paymentId)
    {
        await _mediator.Send(new DeletePaymentFromContractCommand(id, paymentId));
        return NoContent();
    }
    
    [HttpGet("{id:guid}/actions")]
    [ProducesResponseType(typeof(PagedResult<ContractActionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActionsOfContract(Guid id, [FromQuery] ActionsOfContractFilterDto dto)
    {
        var actions = await _mediator.Send(new GetActionsOfContractQuery(id, dto));
        return Ok(actions);
    }
    
    [HttpPost("{id:guid}/actions")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> AddActionOfContract(Guid id, ContractActionCreateDto dto)
    {
        await _mediator.Send(new AddActionToContractCommand(id, dto));
        return NoContent();
    }
    
    [HttpPut("{id:guid}/actions/{actionId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateActionOfContract(Guid id, Guid actionId, ContractActionUpdateDto dto)
    {
        await _mediator.Send(new UpdateContractActionCommand(id, actionId, dto));
        return NoContent();
    }
    
    [HttpDelete("{id:guid}/actions/{actionId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteActionOfContract(Guid id, Guid actionId)
    {
        await _mediator.Send(new DeleteActionFromContractCommand(id, actionId));
        return NoContent();
    }
}
