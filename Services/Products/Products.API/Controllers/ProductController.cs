using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Commands.ProductCommands.AddProduct;
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
    public async Task<IActionResult> GetProducts([FromQuery] GetProductsWithPaginationQuery query)
    {
        var products = await _mediator.Send(query);
        return Ok(products);
    } 
    
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] AddProductCommand command)
    {
        var product = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetProducts), new {id = product.Id}, product);
    }
}