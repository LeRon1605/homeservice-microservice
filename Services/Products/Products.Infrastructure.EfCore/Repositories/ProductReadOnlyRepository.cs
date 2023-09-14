using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Products.Domain.ProductAggregate;
using Products.Infrastructure.EfCore.Data;

namespace Products.Infrastructure.EfCore.Repositories;

public class ProductReadOnlyRepository : EfCoreReadOnlyRepository<ProductDbContext, Product>
{
    public ProductReadOnlyRepository(ProductDbContext dbContext) : base(dbContext)
    {
    }
}