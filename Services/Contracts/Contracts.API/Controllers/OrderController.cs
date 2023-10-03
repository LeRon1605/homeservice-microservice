﻿using Contracts.Application.Commands.Contracts.ConvertContractFromOrder;
using Contracts.Application.Dtos.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Contracts.API.Controllers;

[Route("api/orders")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("{id:guid}/contract")]
    [ProducesResponseType(typeof(ContractDetailDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> ConvertOrderToContract(Guid id, ContractCreateDto dto)
    {
        var contract = await _mediator.Send(new ConvertContractFromOrderCommand(id, dto));
        return Ok(contract);
    }
}