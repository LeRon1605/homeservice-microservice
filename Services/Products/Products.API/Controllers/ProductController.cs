using BuildingBlocks.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Commands.ProductCommands.AddProduct;
using Products.Application.Commands.ProductCommands.DeleteProduct;
using Products.Application.Commands.ProductCommands.UpdateProduct;
using Products.Application.Commands.ProductCommands.UploadProductImage;
using Products.Application.Dtos;
using Products.Application.Dtos.Materials;
using Products.Application.Dtos.Products;
using Products.Application.Queries.MaterialQuery.GetMaterialByProduct;
using Products.Application.Queries.ProductQuery.GetProductById;
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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        return Ok(product);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProductAsync([FromBody] ProductCreateDto productCreateDto)
    {
        var product = await _mediator.Send(new AddProductCommand(productCreateDto));
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    } 
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(GetProductDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateProductAsync(Guid id, ProductUpdateDto productUpdateDto)
    {
        var product = await _mediator.Send(new UpdateProductCommand(id, productUpdateDto));
        return Ok(product);
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

    [HttpGet("{id:guid}/materials")]
    [ProducesResponseType(typeof(PagedResult<GetMaterialDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMaterialsByProductAsync(Guid id, [FromQuery] MaterialFilterAndPagingByProductDto dto)
    {
        var materials = await _mediator.Send(new GetMaterialByProductQuery(id, dto));
        return Ok(materials);
    }
}