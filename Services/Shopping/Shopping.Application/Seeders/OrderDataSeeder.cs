using Bogus;
using BuildingBlocks.Application.Seeder;
using BuildingBlocks.Domain.Data;
using Microsoft.Extensions.Logging;
using Shopping.Domain.OrderAggregate;

namespace Shopping.Application.Seeders;

public class OrderDataSeeder : IDataSeeder
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrderDataSeeder> _logger;

    public OrderDataSeeder(IRepository<Order> orderRepository, IUnitOfWork unitOfWork, ILogger<OrderDataSeeder> logger)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        if (await _orderRepository.AnyAsync())
        {
            _logger.LogInformation("No need to seed data!");
            return;
        }

        try
        {
            _logger.LogTrace("Begin seeding product reviews...");

            var faker = new Faker();
            for (var i = 0; i < 10; i++)
            {
                var order = new Order(
                    $"ORDER-{i}",
                    faker.Name.FindName(),
                    faker.Phone.PhoneNumber(),
                    faker.Internet.Email(),
                    faker.Finance.Amount(),
                    faker.Date.Past(),
                    faker.PickRandom<ProductStatus>()
                );
                _orderRepository.Add(order);
                await _unitOfWork.SaveChangesAsync();
            }

            _logger.LogTrace("Seed product reviews successfully!");
        }
        catch
        {
            _logger.LogTrace("Seed product reviews failed!");
        }
    }
}