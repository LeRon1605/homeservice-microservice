using Bogus;
using Customers.Domain.CustomerAggregate;
using Microsoft.EntityFrameworkCore;

namespace Customers.Infrastructure.EfCore
{
	public static class InitData
	{
		public static void SeedData(this ModelBuilder modelBuilder)
		{
			var customerFaker = new Faker<Customer>()
				.RuleFor(p => p.Id, f => Guid.NewGuid())
				.RuleFor(p => p.CustomerName, f => f.Name.FullName());


			var customers = customerFaker.Generate(10);
			modelBuilder.Entity<Customer>().HasData(customers);
		}
	}
}
