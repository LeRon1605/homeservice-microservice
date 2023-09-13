using Bogus;
using Microsoft.EntityFrameworkCore;
using Shopping.Domain.ShoppingAggregate;

namespace Shopping.Infrastructure.EfCore;

public static class InitData
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        var customerFaker = new Faker<Order>()
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.Name, f => f.Name.FullName());


        var orders = customerFaker.Generate(10);
        modelBuilder.Entity<Order>().HasData(orders);
    }
}