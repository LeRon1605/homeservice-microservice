using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Products.Domain.ProductTypeAggregate;
using Products.Infrastructure.EfCore.Data;

namespace Products.Infrastructure.EfCore.Repositories;

public class ProductTypeReadOnlyRepository : EfCoreReadOnlyRepository<ProductDbContext, ProductType>
{
    public ProductTypeReadOnlyRepository(ProductDbContext dbContext) : base(dbContext)
    {
    }
}