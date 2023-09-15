using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Products.Domain.ProductGroupAggregate;
using Products.Infrastructure.EfCore.Data;

namespace Products.Infrastructure.EfCore.Repositories;

public class ProductGroupReadOnlyRepository : EfCoreReadOnlyRepository<ProductDbContext, ProductGroup>
{
    public ProductGroupReadOnlyRepository(ProductDbContext dbContext) : base(dbContext)
    {
    }
}