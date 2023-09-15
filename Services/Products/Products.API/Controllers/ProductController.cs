using BuildingBlocks.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Commands.ProductCommands.AddProduct;
using Products.Application.Commands.ProductCommands.DeleteProduct;
using Products.Application.Commands.ProductCommands.UploadProductImage;
using Products.Application.Dtos;
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
    
    [HttpPost]
    public async Task<IActionResult> CreateProductAsync([FromBody] ProductCreateDto productCreateDto)
    {
        var product = await _mediator.Send(new AddProductCommand(productCreateDto));
        return Ok(product);
        // return CreatedAtAction(nameof(GetProductbyId), new { id = product.Id }, product);
    } 

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteProductAsync(Guid id)
    {
        await _mediator.Send(new DeleteProductCommand(id));
        return NoContent();
    }
    
    [HttpPost("images")]
    public async Task<IActionResult> UploadProductImageAsync([FromForm] IFormFile file)
    {
        var result = await _mediator.Send(new UploadProductImageCommand(file));
        return Ok(result);
    }
}