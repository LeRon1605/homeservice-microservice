using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Dtos;
using Products.Application.Dtos.Products;
using Products.Application.Queries.ProductQuery.GetAllProductType;

namespace Products.API.Controllers;

[ApiController]
[Route("api/product-types")]
public class ProductTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductTypeDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductTypesAsync([FromQuery] GetAllProductTypeQuery query)
    {
        var productTypes = await _mediator.Send(query);
        return Ok(productTypes);
    }
    
    
}