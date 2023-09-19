using System.Linq.Expressions;
using BuildingBlocks.Domain.Specification;

namespace Products.Domain.ProductAggregate.Specifications;

public class ProductByIdSpecification : Specification<Product>
{
    private readonly Guid _productId;

    public ProductByIdSpecification(Guid productId)
    {
        _productId = productId;
        AddInclude(x => x.Images);
        AddInclude(x => x.Group);
        AddInclude(x => x.BuyUnit);
        AddInclude(x => x.SellUnit);
        AddInclude(x => x.Type);
    }
    public override Expression<Func<Product, bool>> ToExpression()
    {
        return p => p.Id == _productId;
    }
}