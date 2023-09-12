using BuildingBlocks.Domain.Data;
using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Products.Domain.ProductAggregate;
using Products.Infrastructure.Data;

namespace Products.Infrastructure.Repositories;

public class ProductReadonlyRepository : EfCoreReadOnlyRepository<ProductDbContext, Product>
{
    public ProductReadonlyRepository(ProductDbContext dbContext) : base(dbContext)
    {
    }
}