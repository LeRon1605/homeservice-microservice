using ApiGateway.Dtos.Products;
using ApiGateway.Services.Interfaces;
using BuildingBlocks.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers.Bff.Customer;

[Route("api/customer-bff/product-management/products")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet]
    public async Task<ActionResult<PagedResult<ProductData>>> GetProductAsync([FromQuery] GetProductWithFilterAndPaginationDto dto)
    {
        var products = await _productService.GetPagedAsync(dto);
        return Ok(products);
    }
}