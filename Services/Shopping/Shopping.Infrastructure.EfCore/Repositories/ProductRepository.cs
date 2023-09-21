using BuildingBlocks.Infrastructure.EfCore;
using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Shopping.Domain.ProductAggregate;

namespace Shopping.Infrastructure.EfCore.Repositories;

public class ProductRepository : EfCoreRepository<Product>, IProductRepository
{
    public ProductRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }
}