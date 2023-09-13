using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Products.Domain.ProductAggregate;
using Products.Infrastructure.EfCore.Data;

namespace Products.Infrastructure.EfCore.Repositories;

public class ProductRepository : EfCoreRepository<ProductDbContext, Product>
{
    public ProductRepository(ProductDbContext dbContext) : base(dbContext)
    {
    }
}