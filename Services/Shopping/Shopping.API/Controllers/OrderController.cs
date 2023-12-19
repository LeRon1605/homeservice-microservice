using BuildingBlocks.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Application.Commands.Orders.ExportOrders;
using Shopping.Application.Commands.Orders.RejectOrder;
using Shopping.Application.Commands.Orders.SubmitOrder;
using Shopping.Application.Dtos.Orders;
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
    [Authorize(Roles = "Admin, Sales person, Customer service, Installer, Supervisor, Customer")]
    [ProducesResponseType(typeof(PagedResult<OrderDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrdersAsync([FromQuery] OrderFilterAndPagingDto orderFilterAndPagingDto)
    {
        var orders = await _mediator.Send(new OrderFilterAndPagingQuery(orderFilterAndPagingDto));
        return Ok(orders);
    }
    
    [HttpPost]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> SubmitOrderAsync(OrderSubmitDto dto)
    {
        await _mediator.Send(new SubmitOrderCommand(dto.Items));
        return Ok();
    }

   [HttpPost("{id:guid}/reject-order")]
   [Authorize(Roles = "Admin, Sales person, Customer service, Installer, Supervisor")]
   [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> RejectOrder(Guid id, [FromBody]OrderRejectDto orderRejectDto)
    {
        var order = await _mediator.Send(new OrderRejectCommand(id, orderRejectDto));
        return Ok(order);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin, Sales person, Customer service, Installer, Supervisor, Customer")]
    [ProducesResponseType(typeof(PagedResult<OrderDetailsDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrderDetails(Guid id)
    {
        var orderDetails = await _mediator.Send(new OrderDetailQuery(id));
        return Ok(orderDetails);
    }

    [HttpGet("export")]
    [Authorize(Roles = "Admin, Sales person, Customer service, Installer, Supervisor")]
    public async Task<IActionResult> ExportOrdersAsync()
    {
        var byteArray = await _mediator.Send(new ExportOrdersCommand());
        
        return File(byteArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "orders.xlsx");
    }
}