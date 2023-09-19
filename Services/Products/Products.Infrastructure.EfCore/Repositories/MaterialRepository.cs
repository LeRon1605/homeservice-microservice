using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Products.Domain.MaterialAggregate;
using Products.Infrastructure.EfCore.Data;

namespace Products.Infrastructure.EfCore.Repositories;

public class MaterialRepository : EfCoreRepository<ProductDbContext, Material>
{
    public MaterialRepository(ProductDbContext dbContext) : base(dbContext)
    {
    }
}