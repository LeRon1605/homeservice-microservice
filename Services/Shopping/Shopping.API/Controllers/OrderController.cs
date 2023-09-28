using BuildingBlocks.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopping.Application.Commands;
using Shopping.Application.Dtos;
using Shopping.Application.Queries;

namespace Shopping.API.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<OrderDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrdersAsync([FromQuery] OrderFilterAndPagingDto orderFilterAndPagingDto)
    {
        var orders = await _mediator.Send(new OrderFilterAndPagingQuery(orderFilterAndPagingDto));
        return Ok(orders);
    }

   [HttpPost("reject-order")]
    public async Task<IActionResult> RejectOrder(OrderRejectDto orderRejectDto)
    {
        var order = await _mediator.Send(new OrderRejectCommand(orderRejectDto));
        return Ok(order);
    }
}