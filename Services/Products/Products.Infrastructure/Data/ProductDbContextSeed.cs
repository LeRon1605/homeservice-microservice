using Products.Domain.ProductAggregate;

namespace Products.Infrastructure.Data;

public static class ProductDbContextSeed
{
    public static async Task SeedAsync(ProductDbContext context)
    {
        if (!context.Products.Any())
        {
            context.Products.AddRange(new List<Product>
            {
                new("Product 1"),
                new("Product 2"),
                new("Product 3"),
            });
            
            await context.SaveChangesAsync();
        }
    }
}