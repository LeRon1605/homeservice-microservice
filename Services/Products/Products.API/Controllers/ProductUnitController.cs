using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Dtos;
using Products.Application.Dtos.Products;
using Products.Application.Queries.ProductQuery.GetAllProductUnit;
using Products.Application.Queries.ProductQuery.GetProductUnitById;

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

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductUnitDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductUnitByIdAsync(Guid id)
    {
        var productUnit = await _mediator.Send(new GetProductUnitByIdQuery(id));
        return Ok(productUnit);
    }
}