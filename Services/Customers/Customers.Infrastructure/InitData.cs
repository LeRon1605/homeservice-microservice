using Bogus;
using Customers.Domain.CustomerAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Infrastructure
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
