using BuildingBlocks.Application.Dtos;
using Contracts.Application.Commands.Contracts.AddActionToContract;
using Contracts.Application.Commands.Contracts.AddContract;
using Contracts.Application.Commands.Contracts.AddItemToContract;
using Contracts.Application.Commands.Contracts.AddPaymentToContract;
using Contracts.Application.Commands.Contracts.DeleteActionFromContract;
using Contracts.Application.Commands.Contracts.DeleteItemFromContract;
using Contracts.Application.Commands.Contracts.DeletePaymentFromContract;
using Contracts.Application.Commands.Contracts.UpdateContract;
using Contracts.Application.Commands.Contracts.UpdateContractAction;
using Contracts.Application.Commands.Contracts.UpdateContractItem;
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
using Contracts.Application.Queries.Contracts.GetItemsOfContract;
using Contracts.Application.Queries.Contracts.GetPaymentsOfContract;
using Contracts.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(ContractPermission.View)]
    [ProducesResponseType(typeof(PagedResult<ContractDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetContracts([FromQuery] GetContractsQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [Authorize(ContractPermission.View)]
    public async Task<IActionResult> GetContracts(Guid id)
    {
        var result = await _mediator.Send(new GetContractByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    [Authorize(ContractPermission.Add)]
    [ProducesResponseType(typeof(ContractDetailDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateContract(ContractCreateDto dto)
    {
        var contract = await _mediator.Send(new AddContractCommand(dto));
        return Ok(contract);
    }

    [HttpPut("{id:guid}")]
    [Authorize(ContractPermission.Edit)]
    [ProducesResponseType(typeof(ContractDetailDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateContract(Guid id, ContractUpdateDto dto)
    {
        var contract = await _mediator.Send(new UpdateContractCommand(id, dto));
        return Ok(contract);
    }

    [HttpGet("customer/{customerId:guid}")]
    [Authorize(ContractPermission.View)]
    public async Task<IActionResult> GetContractsOfCustomer(Guid customerId, [FromQuery] ContractOfCustomerFilterDto dto)
    {
        var contracts = await _mediator.Send(new ContractsOfCustomerQuery(customerId, dto));
        return Ok(contracts);
    }
    
    [HttpGet("{id:guid}/payments")]
    [Authorize(ContractPermission.View)]
    [ProducesResponseType(typeof(PagedResult<PaymentMethodDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPaymentsOfContract(Guid id, [FromQuery] PaymentsOfContractFilterDto dto)
    {
        var payments = await _mediator.Send(new GetPaymentsOfContractQuery(id, dto));
        return Ok(payments);
    }
    
    [HttpPost("{id:guid}/payments")]
    [Authorize(ContractPermission.Edit)]
    [ProducesResponseType(typeof(ContractPaymentDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddPaymentOfContract(Guid id, ContractPaymentCreateDto dto)
    {
        var contractPayment = await _mediator.Send(new AddPaymentToContractCommand(id, dto));
        return Ok(contractPayment);
    }
    
    [HttpPut("{id:guid}/payments/{paymentId:guid}")]
    [Authorize(ContractPermission.Edit)]
    [ProducesResponseType(typeof(ContractPaymentDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdatePaymentOfContract(Guid id, Guid paymentId, ContractPaymentUpdateDto dto)
    {
        var contractPayment = await _mediator.Send(new UpdateContractPaymentCommand(id, paymentId, dto));
        return Ok(contractPayment);
    }
    
    [HttpDelete("{id:guid}/payments/{paymentId:guid}")]
    [Authorize(ContractPermission.Edit)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeletePaymentOfContract(Guid id, Guid paymentId)
    {
        await _mediator.Send(new DeletePaymentFromContractCommand(id, paymentId));
        return NoContent();
    }
    
    [HttpGet("{id:guid}/actions")]
    [Authorize(ContractPermission.View)]
    [ProducesResponseType(typeof(PagedResult<ContractActionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActionsOfContract(Guid id, [FromQuery] ActionsOfContractFilterDto dto)
    {
        var actions = await _mediator.Send(new GetActionsOfContractQuery(id, dto));
        return Ok(actions);
    }
    
    [HttpPost("{id:guid}/actions")]
    [Authorize(ContractPermission.Edit)]
    [ProducesResponseType(typeof(ContractActionDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddActionOfContract(Guid id, ContractActionCreateDto dto)
    {
        var contractAction = await _mediator.Send(new AddActionToContractCommand(id, dto));
        return Ok(contractAction);
    }
    
    [HttpPut("{id:guid}/actions/{actionId:guid}")]
    [Authorize(ContractPermission.Edit)]
    [ProducesResponseType(typeof(ContractActionDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateActionOfContract(Guid id, Guid actionId, ContractActionUpdateDto dto)
    {
        var contractAction = await _mediator.Send(new UpdateContractActionCommand(id, actionId, dto));
        return Ok(contractAction);
    }
    
    [HttpDelete("{id:guid}/actions/{actionId:guid}")]
    [Authorize(ContractPermission.Edit)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteActionOfContract(Guid id, Guid actionId)
    {
        await _mediator.Send(new DeleteActionFromContractCommand(id, actionId));
        return NoContent();
    }
    
    [HttpGet("{id:guid}/items")]
    [Authorize(ContractPermission.View)]
    [ProducesResponseType(typeof(IEnumerable<ContractLineDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetItemsOfContract(Guid id)
    {
        var items = await _mediator.Send(new GetItemsOfContractQuery(id));
        return Ok(items);
    }
    
    [HttpPost("{id:guid}/items")]
    [Authorize(ContractPermission.Edit)]
    [ProducesResponseType(typeof(ContractLineDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddItemToContract(Guid id, ContractLineCreateDto dto)
    {
        var item = await _mediator.Send(new AddItemToContractCommand(id, dto));
        return Ok(item);
    }
    
    [HttpPut("{id:guid}/items/{itemId:guid}")]
    [Authorize(ContractPermission.Edit)]
    [ProducesResponseType(typeof(ContractLineDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateItemOfContract(Guid id, Guid itemId, ContractLineUpdateDto dto)
    {
        var item = await _mediator.Send(new UpdateContractItemCommand(id, itemId, dto));
        return Ok(item);
    }
    
    [HttpDelete("{id:guid}/items/{itemId:guid}")]
    [Authorize(ContractPermission.Edit)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteItemOfContract(Guid id, Guid itemId)
    {
        await _mediator.Send(new DeleteItemFromContractCommand(id, itemId));
        return NoContent();
    }
}
