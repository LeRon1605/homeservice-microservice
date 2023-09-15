using System.Linq.Expressions;
using BuildingBlocks.Domain.Specification;

namespace Products.Domain.ProductAggregate.Specifications;

public class ProductCodeSpecification : Specification<Product>
{
    private readonly string _productCode;

    public ProductCodeSpecification(string productCode)
    {
        _productCode = productCode;
    }
    public override Expression<Func<Product, bool>> ToExpression()
    {
        return p => (string.IsNullOrWhiteSpace(_productCode) || p.ProductCode.ToLower() == (_productCode.ToLower()));
    }
}