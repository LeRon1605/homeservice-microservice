using Bogus;
using BuildingBlocks.Application.Seeder;
using BuildingBlocks.Domain.Data;
using MediatR;
using Microsoft.Extensions.Logging;
using Polly;
using Shopping.Application.Commands.Buyers.EditBuyer;
using Shopping.Application.Commands.Orders.SubmitOrder;
using Shopping.Domain.BuyerAggregate;
using Shopping.Domain.OrderAggregate;
using Shopping.Domain.ProductAggregate;
using Shopping.Domain.ProductAggregate.Specifications;

namespace Shopping.Application.Seeders;

public class OrderDataSeeder : IDataSeeder
{
    private readonly IMediator _mediator;
    private readonly IRepository<Order> _orderRepository;
    private readonly IReadOnlyRepository<Product> _productRepository;
    private readonly IReadOnlyRepository<Buyer> _buyerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrderDataSeeder> _logger;

    public OrderDataSeeder(
        IMediator mediator,
        IRepository<Order> orderRepository, 
        IUnitOfWork unitOfWork,
        ILogger<OrderDataSeeder> logger, 
        IReadOnlyRepository<Product> productRepository, 
        IReadOnlyRepository<Buyer> buyerRepository)
    {
        _mediator = mediator;
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
            _logger.LogInformation("No need to seed order data!");
            return;
        }
        
        var policy = Policy.Handle<Exception>()
            .WaitAndRetry(10, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (ex, time) =>
                {
                    _logger.LogWarning(ex, "Couldn't seed order table after {TimeOut}s", $"{time.TotalSeconds:n1}");
                }
            );
        
        policy.Execute(() =>
        {
            if (!_buyerRepository.AnyAsync().Result)
            {
                throw new Exception("Buyer data not seeded yet!");
            }
        });

        await SeedBuyersAsync();
        await SeedOrdersAsync();
    }

    private async Task SeedBuyersAsync()
    {
        var buyers = await _buyerRepository.GetAllAsync();
        foreach (var buyer in buyers)
        {
            var faker = new Faker();
            try
            {
                var editBuyerCommand = new EditBuyerCommand(
                    buyer.FullName, 
                    buyer.Email, 
                    faker.Person.Address.City, 
                    faker.Person.Phone, 
                    faker.Person.Address.City, 
                    faker.Person.Address.State, 
                    faker.Person.Address.ZipCode, 
                    faker.Person.Avatar)
                {
                    Id = buyer.Id
                };
             
                await _mediator.Send(editBuyerCommand);
            }
            catch (Exception e)
            {
                _logger.LogError("Edit buyer failed: {Messsage}", e.Message);
            }
        }
    }

    private async Task SeedOrdersAsync()
    {
        _logger.LogTrace("Begin seeding orders...");
        
        var buyers = await _buyerRepository.GetAllAsync();
        var products = await _productRepository.FindListAsync(new ProductIncludeUnitSpecification());

        for (var i = 0; i < 50; i++)
        {
            try
            {
                var faker = new Faker();
                var buyer = buyers[faker.Random.Int(0, buyers.Count - 1)];
                var order = new Order(
                    buyer.Id,
                    buyer.FullName,
                    buyer.Email,
                    buyer.Phone,
                    buyer.Address.FullAddress,
                    buyer.Address.City,
                    buyer.Address.State,
                    buyer.Address.PostalCode
                );

                SeedOrderLines(order, products);

                switch (faker.Random.Int(0, 2))
                {
                    case 0:
                        order.Reject();
                        break;
                }

                _orderRepository.Add(order);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogTrace("Seed orders successfully!");
            }
            catch (Exception e)
            {
                _logger.LogTrace("Seed orders failed: {Message}", e.Message);
            }
        }
    }
    
    private void SeedOrderLines(Order order, IList<Product> products)
    {
        for (var i = 0; i < 3; i++)
        {
            var faker = new Faker();
            var product = products[faker.Random.Int(0, products.Count - 1)];
            
            order.AddOrderLine(
                product.Id,
                product.Name,
                product.ProductUnitId,
                product.ProductUnit?.Name,
                faker.Commerce.Color(),
                faker.Random.Int(1, 10),
                product.Price
            );
        }
    }
}