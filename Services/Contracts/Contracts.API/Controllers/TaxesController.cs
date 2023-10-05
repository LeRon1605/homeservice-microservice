using Contracts.Application.Dtos.Taxes;
using Contracts.Application.Queries.Taxes.GetAllTaxes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Contracts.API.Controllers;

[Route("api/taxes")]
[ApiController]
public class TaxesController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public TaxesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TaxDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTaxes()
    {
        var result = await _mediator.Send(new GetAllTaxesQuery());
        return Ok(result);
    }
}