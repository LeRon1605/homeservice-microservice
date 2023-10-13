using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Dtos;
using Products.Application.Dtos.Products;
using Products.Application.Queries.ProductQuery.GetAllProductUnit;

namespace Products.API.Controllers;

[ApiController]
[Route("api/product-units")]
public class ProductUnitController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductUnitController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductUnitDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductUnitsAsync([FromQuery] GetAllProductUnitQuery query)
    {
        var productUnits = await _mediator.Send(query);
        return Ok(productUnits);
    }
}