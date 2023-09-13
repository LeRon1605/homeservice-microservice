using Customers.Application.Dtos;
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
    public async Task<IActionResult> GetCustomersAsync([FromQuery] CustomerFilterAndPagingDto customerFilterAndPagingDto)
    {
        var customers = await _mediator.Send(new CustomerFilterAndPagingDto());
        return Ok(customers);
    }
}