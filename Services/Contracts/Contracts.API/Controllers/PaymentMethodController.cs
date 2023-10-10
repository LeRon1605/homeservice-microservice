using Contracts.Application.Dtos.PaymentMethods;
using Contracts.Application.Queries.PaymentMethods.GetAllPaymentMethods;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Contracts.API.Controllers;

[Route("payment-methods")]
[ApiController]
public class PaymentMethodController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public PaymentMethodController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PaymentMethodDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPaymentMethods()
    {
        var paymentMethods = await _mediator.Send(new GetAllPaymentMethodsQuery());
        return Ok(paymentMethods);
    }
}