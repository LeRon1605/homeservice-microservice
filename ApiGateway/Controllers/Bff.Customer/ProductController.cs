using ApiGateway.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers.Bff.Customer;

[Route("api/customer-bff/product-management")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        await _productService.GetPagedAsync();
        return Ok("Customer BFF Product Management");
    }
}