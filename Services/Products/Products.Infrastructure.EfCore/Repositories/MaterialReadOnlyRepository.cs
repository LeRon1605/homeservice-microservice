using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Products.Domain.ProductAggregate;
using Products.Infrastructure.EfCore.Data;

namespace Products.Infrastructure.EfCore.Repositories;

public class MaterialReadOnlyRepository : EfCoreReadOnlyRepository<ProductDbContext, Product>
{
    public MaterialReadOnlyRepository(ProductDbContext dbContext) : base(dbContext)
    {
    }
}