using Bogus;
using BuildingBlocks.Application.Seeder;
using BuildingBlocks.Domain.Data;
using Microsoft.Extensions.Logging;
using Shopping.Domain.OrderAggregate;
using Shopping.Domain.ProductAggregate;

namespace Shopping.Application.Seeders;

public class OrderDataSeeder : IDataSeeder
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IReadOnlyRepository<Product> _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrderDataSeeder> _logger;

    public OrderDataSeeder(IRepository<Order> orderRepository, IUnitOfWork unitOfWork,
        ILogger<OrderDataSeeder> logger, IReadOnlyRepository<Product> productRepository)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _productRepository = productRepository;
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
            _logger.LogTrace("Begin seeding orders...");

            var faker = new Faker();
            for (var i = 0; i < 10; i++)
            {
                var order = new Order(
                    $"ORDER-{i}",
                    faker.Name.FindName(),
                    faker.Random.Guid(),
                    faker.Phone.PhoneNumber(),
                    faker.Internet.Email(),
                    faker.Finance.Amount()
                );
                var products = (await _productRepository.GetAllAsync()).ToList();
                
                await SeedOrderLinesAsync(order);

                _orderRepository.Add(order);
                await _unitOfWork.SaveChangesAsync();
            }

            _logger.LogTrace("Seed orders successfully!");
        }
        catch
        {
            _logger.LogTrace("Seed orders failed!");
        }
    }
    
    private async Task SeedOrderLinesAsync(Order order)
    {
        var products = (await _productRepository.GetAllAsync()).ToList();
        var faker = new Faker();
        for (int i = 0; i < 3; i++)
        {
            order.AddOrderLine(products.ToList()[faker.Random.Int(0, products.Count - 1)].Id,
                order.Id,
                faker.Random.String(6),
                faker.Random.Int(10),
                faker.Random.Double(0,1000),
                faker.Random.Decimal(0,100000)
                );
        }
    }
}