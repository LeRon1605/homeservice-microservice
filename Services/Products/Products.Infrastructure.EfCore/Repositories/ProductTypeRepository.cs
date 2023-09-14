using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Products.Domain.ProductTypeAggregate;
using Products.Infrastructure.EfCore.Data;

namespace Products.Infrastructure.EfCore.Repositories;

public class ProductTypeRepository : EfCoreRepository<ProductDbContext, ProductType>
{
    public ProductTypeRepository(ProductDbContext dbContext) : base(dbContext)
    {
    }
}