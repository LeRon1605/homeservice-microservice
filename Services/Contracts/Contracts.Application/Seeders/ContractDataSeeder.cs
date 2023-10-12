using Bogus;
using BuildingBlocks.Application.Seeder;
using BuildingBlocks.Domain.Data;
using Contracts.Application.Commands.Contracts.AddContract;
using Contracts.Application.Commands.Contracts.UpdateContract;
using Contracts.Application.Commands.Customers.AddCustomer;
using Contracts.Application.Dtos.Contracts;
using Contracts.Application.Queries.Contracts.GetPaymentsOfContract;
using Contracts.Domain.ContractAggregate;
using Contracts.Domain.ContractAggregate.Specifications;
using Contracts.Domain.CustomerAggregate;
using Contracts.Domain.MaterialAggregate;
using Contracts.Domain.PaymentMethodAggregate;
using Contracts.Domain.ProductAggregate;
using Contracts.Domain.ProductUnitAggregate;
using Contracts.Domain.TaxAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.Seeders;

public class ContractDataSeeder : IDataSeeder
{
    private readonly IReadOnlyRepository<Contract> _contractRepository;
    private readonly IReadOnlyRepository<Customer> _customerRepository;
    private readonly IReadOnlyRepository<Product> _productRepository;
    private readonly IReadOnlyRepository<Material> _materialRepository;
    private readonly IRepository<Tax> _taxRepository;
    private readonly IRepository<PaymentMethod> _paymentMethodRepository;
    private readonly IReadOnlyRepository<PaymentMethod> _paymentMethodReadOnlyRepository;
    private readonly IMediator _mediator;
    private readonly ILogger<ContractDataSeeder> _logger;
    private readonly IReadOnlyRepository<ProductUnit> _productUnitRepository;
    private readonly IReadOnlyRepository<ContractPayment> _contractPaymentRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public ContractDataSeeder(
        IMediator mediator, 
        IRepository<Tax> taxRepository,
        IRepository<PaymentMethod> paymentMethodRepository,
        IReadOnlyRepository<Contract> contractRepository, 
        IReadOnlyRepository<Customer> customerRepository,
        IReadOnlyRepository<Product> productRepository,
        IReadOnlyRepository<Material> materialRepository,
        ILogger<ContractDataSeeder> logger,
        IReadOnlyRepository<ProductUnit> productUnitRepository,
        IReadOnlyRepository<PaymentMethod> paymentMethodReadOnlyRepository,
        IReadOnlyRepository<ContractPayment> contractPaymentRepository,
        IUnitOfWork unitOfWork)
    {
        _taxRepository = taxRepository;
        _paymentMethodRepository = paymentMethodRepository;
        _productRepository = productRepository;
        _materialRepository = materialRepository;
        _mediator = mediator;
        _contractRepository = contractRepository;
        _customerRepository = customerRepository;
        _logger = logger;
        _productUnitRepository = productUnitRepository;
        _unitOfWork = unitOfWork;
        _paymentMethodReadOnlyRepository = paymentMethodReadOnlyRepository;
        _contractPaymentRepository = contractPaymentRepository;
    }
    
    public async Task SeedAsync()
    {
        try
        {
            await SeedPaymentMethodsAsync();
            await SeedTaxesAsync();
            await SeedContractsAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("Seeding data failed: {Message}", e.Message);
        }
    }

    private async Task SeedContractsAsync()
    {
        if (!await _contractRepository.AnyAsync() && await _productRepository.AnyAsync())
        {
            _logger.LogInformation("Begin seeding contracts data...");
            
            var products = await _productRepository.GetAllAsync();
            var productUnits = await _productUnitRepository.GetAllAsync();
            var materials = await _materialRepository.GetAllAsync();
            var customers = await _customerRepository.GetAllAsync();
            var paymentMethods = await _paymentMethodReadOnlyRepository.GetAllAsync();
            
            for (var i = 0; i < 50; i++)
            {
                try
                {
                    var command = new AddContractCommand(GetContractCreateDto(productUnits, products, materials, customers, paymentMethods));
                    await _mediator.Send(command);
                }
                catch(Exception e)
                {
                    _logger.LogError("Seeding a contract failed: {Message}", e.Message);
                }
            }

            var contracts = await _contractRepository.GetAllAsync();
            for (var i = 0; i < 10; i++)
            {
                try
                {
                    var contractUpdateDto = await GetContractUpdateDto(contracts[i], productUnits, products, customers);
                    var command = new UpdateContractCommand(contracts[i].Id, contractUpdateDto);
                    await _mediator.Send(command);
                }
                catch(Exception e)
                {
                    _logger.LogError("Seeding a deleted contract failed: {Message}", e.Message);
                }
            }
            _logger.LogInformation("Finished seeding contracts data...");
        }
    }

    private ContractCreateDto GetContractCreateDto(
        IList<ProductUnit> productUnits,
        IList<Product> products,
        IList<Material> materials,
        IList<Customer> customers,
        IList<PaymentMethod> paymentMethods)
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
            Installations = new List<InstallationCreateDto>(),
            Payments = new List<ContractPaymentCreateDto>(),
            Actions = new List<ContractActionCreateDto>(),
            Status = faker.PickRandom<ContractStatus>()
        };

        for (var j = 0; j < 3; j++)
        {
            var product = products[faker.Random.Int(0, products.Count - 1)];
            var productUnit = productUnits[faker.Random.Int(0, productUnits.Count - 1)];
            var material = materials[faker.Random.Int(0, materials.Count - 1)];
            
            contractCreateDto.Items.Add(new ContractLineCreateDto()
            {
                ProductId = product.Id,
                Quantity = faker.Random.Int(1, 10),
                Cost = product.Price,
                SellPrice = faker.Random.Decimal(100, 1000),
                Color = product.Colors[0],
                UnitId = productUnit.Id
            });
            
            contractCreateDto.Installations.Add(new InstallationCreateDto
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Color = product.Colors[0],
                InstallDate = faker.Date.Soon(40),
                EstimatedStartTime = faker.Date.Soon(40).AddHours(faker.Random.Int(8, 12)),
                EstimatedFinishTime = faker.Date.Soon(40).AddHours(faker.Random.Int(12, 17)),
                ActualStartTime = faker.Date.Soon(40).AddHours(faker.Random.Int(8, 12)),
                ActualFinishTime = faker.Date.Soon(40).AddHours(faker.Random.Int(12, 17)),
                InstallationComment = faker.Lorem.Sentence(),
                FloorType = faker.Random.ListItem(new List<string> {"Concrete", "Wood", "Tile"}),
                // Todo: Get all installer ids
                InstallerId = faker.Random.Guid(),
                InstallationMetres = faker.Random.Double(10, 100),
                Items = new List<InstallationItemCreateDto>
                {
                    new()
                    {
                        MaterialId = material.Id,
                        MaterialName = material.Name,
                        Quantity = faker.Random.Int(1, 10),
                        UnitId = productUnit.Id,
                        UnitName = productUnit.Name,
                        Cost = faker.Random.Decimal(10, 100),
                        SellPrice = faker.Random.Decimal(100, 1000)
                    } 
                }
            });
        }
        
        for (var j = 0; j < 25; j++)
        {
            contractCreateDto.Payments.Add(new ContractPaymentCreateDto()
            {
                PaidAmount = decimal.Parse(faker.Commerce.Price()),
                Reference = faker.Commerce.ProductDescription(),
                Comments = faker.Commerce.ProductDescription(),
                Surcharge = decimal.Parse(faker.Commerce.Price()),
                PaymentMethodId = paymentMethods[faker.Random.Int(0, paymentMethods.Count - 1)].Id,
                DatePaid = faker.Date.Past()
            });
        }

        for (var j = 0; j < 25; j++)
        {
            contractCreateDto.Actions.Add(new ContractActionCreateDto()
            {
                Name = faker.Commerce.ProductName(),
                Comment = faker.Commerce.ProductDescription(),
                Date = faker.Date.Past(),
                ActionByEmployeeId = Guid.NewGuid()
            });
        }

        return contractCreateDto;
    }

    private async Task<ContractUpdateDto> GetContractUpdateDto(
        Contract contract,
        IList<ProductUnit> productUnits,
        IList<Product> products,
        IList<Customer> customers)
    {
        var faker = new Faker();
        var payments = await _contractPaymentRepository.FindListAsync(new PaymentOfContractSpecification(string.Empty, 10, 1, contract.Id,true));
        var customer = customers[faker.Random.Int(0, customers.Count - 1)];
        
        var contractUpdateDto = new ContractUpdateDto()
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
            Items = new List<ContractLineUpdateDto>(),
            Payments = new List<ContractPaymentUpdateDto>(),
            Actions = new List<ContractActionUpdateDto>(),
            Status = faker.PickRandom<ContractStatus>()
        };

        for (var j = 0; j < 3; j++)
        {
            var product = products[faker.Random.Int(0, products.Count - 1)];
            var productUnit = productUnits[faker.Random.Int(0, productUnits.Count - 1)];
            
            contractUpdateDto.Items.Add(new ContractLineUpdateDto()
            {
                ProductId = product.Id,
                Quantity = faker.Random.Int(1, 10),
                Cost = product.Price,
                SellPrice = faker.Random.Decimal(100, 1000),
                Color = product.Colors[0],
                UnitId = productUnit.Id
            });
        }

        var paymentMethods = await _paymentMethodReadOnlyRepository.GetAllAsync();
        for (var j = 0; j < 25; j++)
        {
            contractUpdateDto.Payments.Add(new ContractPaymentUpdateDto()
            {
                PaidAmount = decimal.Parse(faker.Commerce.Price()),
                Reference = faker.Commerce.ProductDescription(),
                Comments = faker.Commerce.ProductDescription(),
                Surcharge = decimal.Parse(faker.Commerce.Price()),
                PaymentMethodId = paymentMethods[faker.Random.Int(0, paymentMethods.Count - 1)].Id,
                DatePaid = faker.Date.Past()
            });
        }
        
        foreach (var payment in payments)
        {
            contractUpdateDto.Payments.Add(new ContractPaymentUpdateDto()
            {
                Id = payment.Id,
                Comments = payment.Comments,
                Reference = payment.Reference,
                DatePaid = payment.DatePaid,
                PaidAmount = payment.PaidAmount,
                PaymentMethodId = payment.PaymentMethodId,
                Surcharge = payment.Surcharge,
                IsDelete = faker.Random.Bool()
            });
        }

        return contractUpdateDto;
    }

    private async Task SeedTaxesAsync()
    {
        if (!await _taxRepository.AnyAsync())
        {
            _logger.LogInformation("Begin seeding taxes data...");

            var taxes = new List<Tax>()
            {
                new("5%"),
                new("10%"),
                new("15%"),
                new("20%")
            };

            foreach (var tax in taxes)
            {
                _taxRepository.Add(tax);
            }

            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Finished seeding customers data...");
        }
    }
    
    private async Task SeedPaymentMethodsAsync()
    {
        if (!await _paymentMethodRepository.AnyAsync())
        {
            _logger.LogInformation("Begin seeding payment methods data...");

            var paymentMethods = new List<PaymentMethod>()
            {
                new("Cash"),
                new("Visa"),
                new("Mastercard"),
                new("American Express")
            };

            foreach (var method in paymentMethods)
            {
                _paymentMethodRepository.Add(method);
            }

            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Finished seeding payment methods data...");
        }
    }
}