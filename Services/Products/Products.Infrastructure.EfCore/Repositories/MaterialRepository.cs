using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Products.Domain.ProductAggregate;
using Products.Infrastructure.EfCore.Data;

namespace Products.Infrastructure.EfCore.Repositories;

public class MaterialRepository : EfCoreRepository<ProductDbContext, Product>
{
    public MaterialRepository(ProductDbContext dbContext) : base(dbContext)
    {
    }
}