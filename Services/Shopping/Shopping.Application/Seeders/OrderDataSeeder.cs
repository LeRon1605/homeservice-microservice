using Bogus;
using BuildingBlocks.Application.Seeder;
using BuildingBlocks.Domain.Data;
using MediatR;
using Microsoft.Extensions.Logging;
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
        
        try
        {
            _logger.LogTrace("Begin seeding orders...");
            
            var buyers = await _buyerRepository.GetAllAsync();
            
            for (var i = 0; i < 50; i++)
            {
                var faker = new Faker();
                
                var buyer = buyers[faker.Random.Int(0, buyers.Count - 1)];
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
                var editedBuyer = await _mediator.Send(editBuyerCommand);
                
                var order = new Order(
                    buyer.Id,
                    editedBuyer.FullName,
                    editedBuyer.Email,
                    editedBuyer.Phone,
                    editedBuyer.Address,
                    editedBuyer.City,
                    editedBuyer.State,
                    editedBuyer.PostalCode
                );
                
                await SeedOrderLinesAsync(order);

                switch (faker.Random.Int(0, 2))
                {
                    case 0:
                        order.Reject();
                        break;
                }
        
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
        var products = await _productRepository.FindListAsync(new ProductIncludeUnitSpecification());
        
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