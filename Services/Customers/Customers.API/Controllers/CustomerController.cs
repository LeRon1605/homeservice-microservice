﻿using BuildingBlocks.Application.Dtos;
using Customers.Application.Commands;
using Customers.Application.Dtos;
using Customers.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Customers.API.Controllers;

[Route("api/customers")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<CustomerDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomersAsync([FromQuery] CustomerFilterAndPagingQuery query)
    {
        var customers = await _mediator.Send(query);
        return Ok(customers);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomerAsync([FromRoute] Guid id)
    {
        var customer = await _mediator.Send(new CustomerByIdQuery(id));
        return Ok(customer);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCustomerAsync([FromBody] CustomerCreateDto customerCreateDto)
    {
        var customer = await _mediator.Send(new AddCustomerCommand(customerCreateDto));
        return Ok(customer);
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteCustomerAsync([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteCustomerCommand(id));
        return NoContent();
    }
    
    
}