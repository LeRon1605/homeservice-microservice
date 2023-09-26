using BuildingBlocks.Domain.Specification;

namespace Shopping.Domain.ProductAggregate.Specifications;

public class ProductByIdSpecification : Specification<Product>
{
    public ProductByIdSpecification(Guid id)
    {
        AddFilter(product => product.Id == id);
    } 
}