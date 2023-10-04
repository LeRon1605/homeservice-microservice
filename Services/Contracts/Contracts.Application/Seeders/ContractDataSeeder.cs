using Bogus;
using BuildingBlocks.Application.Seeder;
using BuildingBlocks.Domain.Data;
using Contracts.Application.Commands.Contracts.AddContract;
using Contracts.Application.Commands.Customers.AddCustomer;
using Contracts.Application.Dtos.Contracts;
using Contracts.Domain.ContractAggregate;
using Contracts.Domain.CustomerAggregate;
using Contracts.Domain.ProductAggregate;
using Contracts.Domain.ProductUnitAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.Seeders;

public class ContractDataSeeder : IDataSeeder
{
    private readonly IRepository<Contract> _contractRepository;
    private readonly IReadOnlyRepository<Customer> _customerRepository;
    private readonly IReadOnlyRepository<Product> _productRepository;
    private readonly IMediator _mediator;
    private readonly ILogger<ContractDataSeeder> _logger;
    private readonly IReadOnlyRepository<ProductUnit> _productUnitRepository;
    
    public ContractDataSeeder(
        IMediator mediator, 
        IRepository<Contract> contractRepository, 
        IReadOnlyRepository<Customer> customerRepository,
        IReadOnlyRepository<Product> productRepository,
        ILogger<ContractDataSeeder> logger,
        IReadOnlyRepository<ProductUnit> productUnitRepository)
    {
        _productRepository = productRepository;
        _mediator = mediator;
        _contractRepository = contractRepository;
        _customerRepository = customerRepository;
        _logger = logger;
        _productUnitRepository = productUnitRepository;
    }
    
    public async Task SeedAsync()
    {
        try
        {
            await SeedCustomersAsync();
            await SeedContractsAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("Seeding data failed: {Message}", e.Message);
        }
    }

    private async Task SeedContractsAsync()
    {
        if (!await _contractRepository.AnyAsync())
        {
            _logger.LogInformation("Begin seeding contracts data...");
            
            var products = (await _productRepository.GetAllAsync()).ToList();
            var productUnits = (await _productUnitRepository.GetAllAsync()).ToList();
            var customers = (await _customerRepository.GetAllAsync()).ToList();
            
            for (var i = 0; i < 10; i++)
            {
                var faker = new Faker();
                var customer = customers[faker.Random.Int(0, customers.Count - 1)];
                
                var contractCreateDto = new ContractCreateDto()
                {
                    CustomerId = customer.Id,
                    CustomerNote = faker.Lorem.Sentence(),
                    SalePersonId = faker.Random.Guid(),
                    SupervisorId = faker.Random.Guid(),
                    CustomerServiceRepId = faker.Random.Guid(),
                    PurchaseOrderNo = faker.Random.Int(0, 100),
                    InvoiceNo = faker.Random.Int(0, 100),
                    InvoiceDate = faker.Date.Past(),
                    EstimatedInstallationDate = faker.Date.Future(),
                    ActualInstallationDate = faker.Date.Future(),
                    InstallationAddress = new InstallationAddressDto()
                    {
                        FullAddress = faker.Address.FullAddress(),
                        City = faker.Address.City(),
                        PostalCode = faker.Address.ZipCode(),
                        State = faker.Address.State()
                    },
                    Items = new List<ContractLineCreateDto>(),
                    Status = ContractStatus.Quotation
                };

                for (var j = 0; j < 3; j++)
                {
                    var product = products[faker.Random.Int(0, products.Count - 1)];
                    var productUnit = productUnits[faker.Random.Int(0, productUnits.Count - 1)];
                    
                    contractCreateDto.Items.Add(new ContractLineCreateDto()
                    {
                        ProductId = product.Id,
                        Quantity = faker.Random.Int(1, 10),
                        Cost = product.Price,
                        SellPrice = faker.Random.Decimal(100, 1000),
                        Color = product.Colors[0],
                        UnitId = productUnit.Id
                    });
                }
                
                var command = new AddContractCommand(contractCreateDto);
                await _mediator.Send(command);
            }
            
            _logger.LogInformation("Finished seeding contracts data...");
        }
    }

    private async Task SeedCustomersAsync()
    {
        if (!await _customerRepository.AnyAsync())
        {
            _logger.LogInformation("Begin seeding customers data...");
            
            for (var i = 0; i < 10; i++)
            {
                var faker = new Faker();
                var command = new AddCustomerCommand(
                    faker.Person.Company.Name,
                    faker.Person.FullName,
                    faker.Person.Email,
                    faker.Person.Address.City,
                    faker.Person.Address.City,
                    faker.Person.Address.State,
                    faker.Address.ZipCode(),
                    "0905857760");
                await _mediator.Send(command);
            }
            
            _logger.LogInformation("Finished seeding customers data...");
        }
    }
}