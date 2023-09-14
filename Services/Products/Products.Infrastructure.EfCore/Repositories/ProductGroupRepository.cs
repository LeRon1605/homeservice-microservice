using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Products.Domain.ProductGroupAggregate;
using Products.Infrastructure.EfCore.Data;

namespace Products.Infrastructure.EfCore.Repositories;

public class ProductGroupRepository : EfCoreRepository<ProductDbContext, ProductGroup>
{
    public ProductGroupRepository(ProductDbContext dbContext) : base(dbContext)
    {
    }
}