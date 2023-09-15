using BuildingBlocks.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Commands.ProductCommands.DeleteProduct;
using Products.Application.Dtos;
using Products.Application.Queries.ProductQuery.GetAllProductGroup;
using Products.Application.Queries.ProductQuery.GetProductsWithPagination;

namespace Products.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<GetProductDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductsAsync([FromQuery] GetProductsWithPaginationQuery query)
    {
        var products = await _mediator.Send(query);
        return Ok(products);
    } 
    
    [HttpGet("groups")]
    [ProducesResponseType(typeof(IEnumerable<ProductGroupDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductGroupsAsync([FromQuery] GetAllProductGroupQuery query)
    {
        var productGroups = await _mediator.Send(query);
        return Ok(productGroups);
    }
    

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteProductAsync(Guid id)
    {
        await _mediator.Send(new DeleteProductCommand(id));
        return NoContent();
    }
}