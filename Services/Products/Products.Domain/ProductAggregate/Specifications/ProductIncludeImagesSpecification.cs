using System.Linq.Expressions;
using BuildingBlocks.Domain.Specification;

namespace Products.Domain.ProductAggregate.Specifications;

public class ProductIncludeImagesSpecification : Specification<Product>
{
    private readonly Guid _productId;
    public override Expression<Func<Product, bool>> ToExpression()
    {
        return p => p.Id == _productId;
    }

    public ProductIncludeImagesSpecification(Guid productId)
    {
        _productId = productId;
        AddInclude(x => x.Group);
        AddInclude(x => x.Images);
        AddInclude(x => x.BuyUnit);
        AddInclude(x => x.SellUnit);
        AddInclude(x => x.Type);
    }
}
