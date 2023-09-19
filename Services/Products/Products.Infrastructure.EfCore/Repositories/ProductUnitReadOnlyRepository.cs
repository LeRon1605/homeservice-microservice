using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Products.Domain.MaterialAggregate;
using Products.Domain.ProductUnitAggregate;
using Products.Infrastructure.EfCore.Data;

namespace Products.Infrastructure.EfCore.Repositories;

public class ProductUnitReadOnlyRepository : EfCoreReadOnlyRepository<ProductDbContext, ProductUnit>
{
    public ProductUnitReadOnlyRepository(ProductDbContext dbContext) : base(dbContext)
    {
    }
}