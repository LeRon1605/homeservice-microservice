using System.Linq.Expressions;
using BuildingBlocks.Domain.Specification;

namespace Products.Domain.ProductAggregate.Specifications;

public class ProductByIdSpecification : Specification<Product>
{
    public ProductByIdSpecification(Guid productId)
    {
        AddFilter(p => p.Id == productId);
        AddInclude(x => x.Images);
        AddInclude(x => x.Group);
        AddInclude(x => x.BuyUnit);
        AddInclude(x => x.SellUnit);
        AddInclude(x => x.Type);
    }
}