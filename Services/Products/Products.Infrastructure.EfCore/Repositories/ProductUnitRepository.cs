using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Products.Domain.ProductUnitAggregate;
using Products.Infrastructure.EfCore.Data;

namespace Products.Infrastructure.EfCore.Repositories;

public class ProductUnitRepository : EfCoreRepository<ProductDbContext, ProductUnit>
{
    public ProductUnitRepository(ProductDbContext dbContext) : base(dbContext)
    {
    }
}