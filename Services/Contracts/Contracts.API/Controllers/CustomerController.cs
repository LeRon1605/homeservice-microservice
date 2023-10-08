﻿using BuildingBlocks.Application.Dtos;
using Contracts.Application.Commands.Customers.AddCustomer;
using Contracts.Application.Commands.Customers.DeleteCustomer;
using Contracts.Application.Commands.Customers.EditCustomer;
using Contracts.Application.Dtos.Customers;
using Contracts.Application.Queries.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Contracts.API.Controllers;

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
    public async Task<IActionResult> GetCustomers([FromQuery] CustomerFilterAndPagingQuery query)
    {
        var customers = await _mediator.Send(query);
        return Ok(customers);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomerById([FromRoute] Guid id)
    {
        var customer = await _mediator.Send(new CustomerByIdQuery(id));
        return Ok(customer);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] AddCustomerCommand command)
    {
        var customer = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCustomerById), new {id = customer.Id}, customer);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCustomer([FromRoute] Guid id, [FromBody] EditCustomerCommand command)
    {
        command.Id = id;
        var customer = await _mediator.Send(command);
        return Ok(customer);
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteCustomer([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteCustomerCommand(id));
        return NoContent();
    }
    
    
}