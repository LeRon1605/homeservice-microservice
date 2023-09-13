using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Products.Domain.ProductAggregate;
using Products.Infrastructure.EfCore.Data;

namespace Products.Infrastructure.EfCore.Repositories;

public class ProductReadonlyRepository : EfCoreReadOnlyRepository<ProductDbContext, Product>
{
    public ProductReadonlyRepository(ProductDbContext dbContext) : base(dbContext)
    {
    }
}