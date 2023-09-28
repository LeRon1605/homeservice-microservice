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
    private readonly IShoppingService _shoppingService;
    
    public ProductController(IProductService productService,
                             IShoppingService shoppingService)
    {
        _productService = productService;
        _shoppingService = shoppingService;
    }
    
    [HttpGet]
    public ActionResult<PagedResult<ProductData>> GetProductAsync([FromQuery] GetProductWithFilterAndPaginationDto dto)
    {
        var products = _productService.GetPaged(dto);
        return Ok(products);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductData>> GetProductByIdAsync(Guid id)
    {
        var product = await _productService.GetByIdAsync(id);
        var shoppingProduct = _shoppingService.GetShoppingProductById(id);
        
        product.AverageRating = shoppingProduct.AverageRating;
        product.NumberOfRating = shoppingProduct.NumberOfRating;
        product.DiscountPrice = shoppingProduct.DiscountPrice;
        product.NumberOfOrder = shoppingProduct.NumberOfOrder;
        
        return Ok(product);
    }
    
    [HttpGet("included-ids")]
    public async Task<ActionResult<IEnumerable<ProductData>>> GetProductByIncludedIdsAsync([FromQuery] IEnumerable<Guid> ids)
    {
        var products = await _productService.GetProductByIncludedIdsAsync(ids);
        return Ok(products);
    }
}