using Bogus;
using BuildingBlocks.Application.Seeder;
using BuildingBlocks.Domain.Data;
using Microsoft.Extensions.Logging;
using Shopping.Domain.BuyerAggregate;
using Shopping.Domain.OrderAggregate;
using Shopping.Domain.ProductAggregate;
using Shopping.Domain.ProductAggregate.Specifications;

namespace Shopping.Application.Seeders;

public class OrderDataSeeder : IDataSeeder
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IReadOnlyRepository<Product> _productRepository;
    private readonly IReadOnlyRepository<Buyer> _buyerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrderDataSeeder> _logger;

    public OrderDataSeeder(IRepository<Order> orderRepository, IUnitOfWork unitOfWork,
        ILogger<OrderDataSeeder> logger, IReadOnlyRepository<Product> productRepository, IReadOnlyRepository<Buyer> buyerRepository)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _productRepository = productRepository;
        _buyerRepository = buyerRepository;
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
            var buyer = (await _buyerRepository.GetAllAsync()).ToList().First();
            for (var i = 0; i < 10; i++)
            {
           
                var order = new Order(
                    $"ORDER-{i}",
                    buyer.Id,
                    faker.Name.FullName(),
                    faker.Name.FullName(),
                    faker.Internet.Email(),
                    faker.Phone.PhoneNumber(),
                    faker.Address.StreetAddress(),
                    faker.Address.City(),
                    faker.Address.StateAbbr(),
                    faker.Address.ZipCode()
                );
                var products = (await _productRepository.GetAllAsync()).ToList();
                
                await SeedOrderLinesAsync(order);
        
                _orderRepository.Add(order);
            }
            await _unitOfWork.SaveChangesAsync();
        
            _logger.LogTrace("Seed orders successfully!");
        }
        catch (Exception e)
        {
            _logger.LogTrace("Seed orders failed!");
        }
    }
    
    private async Task SeedOrderLinesAsync(Order order)
    {
        var products = (await _productRepository.FindListAsync(new ProductIncludeUnitSpecification())).ToList();
        var faker = new Faker();
        for (var i = 0; i < 3; i++)
        {
            var product = products[faker.Random.Int(0, products.Count - 1)];
            order.AddOrderLine(
                product.Id,
                product.Name,
                product.ProductUnit.Name,
                faker.Random.String(6),
                faker.Random.Int(10),
                faker.Random.Decimal(0,1000)
            );
        }
    }
}