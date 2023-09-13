using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Products.Domain.ProductAggregate;
using Products.Infrastructure.Data;

namespace Products.Infrastructure.Repositories;

public class ProductRepository : EfCoreRepository<ProductDbContext, Product>
{
    public ProductRepository(ProductDbContext dbContext) : base(dbContext)
    {
    }
}