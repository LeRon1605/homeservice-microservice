using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopping.Application.Commands.Buyers.EditBuyer;
using Shopping.Application.Dtos.Buyers;
using Shopping.Application.Queries.BuyerQueries;

namespace Shopping.API.Controllers;

[Route("api/buyers")]
[ApiController]
public class BuyerController : ControllerBase
{
    private readonly IMediator _mediator;

    public BuyerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BuyerDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBuyerById([FromRoute] Guid id)
    {
        var buyer = await _mediator.Send(new BuyerByIdQuery(id));
        return Ok(buyer);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCustomer([FromRoute] Guid id, [FromBody] EditBuyerCommand command)
    {
        command.Id = id;
        var customer = await _mediator.Send(command);
        return Ok(customer);
    }
}