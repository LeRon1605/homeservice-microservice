using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Dtos;
using Products.Application.Queries.ProductQuery.GetAllProductGroup;

namespace Products.API.Controllers;

[ApiController]
[Route("api/product-groups")]
public class ProductGroupController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductGroupController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductGroupDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductGroupsAsync([FromQuery] GetAllProductGroupQuery query)
    {
        var productGroups = await _mediator.Send(query);
        return Ok(productGroups);
    }
    
    
}