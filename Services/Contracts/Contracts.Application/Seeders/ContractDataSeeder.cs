﻿using Bogus;
using BuildingBlocks.Application.Seeder;
using BuildingBlocks.Domain.Data;
using Contracts.Application.Commands.Contracts.AddContract;
using Contracts.Application.Commands.Contracts.UpdateContract;
using Contracts.Application.Commands.Customers.AddCustomer;
using Contracts.Application.Dtos.Contracts;
using Contracts.Application.Dtos.Contracts.ContractCreate;
using Contracts.Application.Dtos.Contracts.ContractUpdate;
using Contracts.Application.Queries.Contracts.GetPaymentsOfContract;
using Contracts.Domain.ContractAggregate;
using Contracts.Domain.ContractAggregate.Specifications;
using Contracts.Domain.CustomerAggregate;
using Contracts.Domain.EmployeeAggregate;
using Contracts.Domain.EmployeeAggregate.Specifications;
using Contracts.Domain.MaterialAggregate;
using Contracts.Domain.PaymentMethodAggregate;
using Contracts.Domain.ProductAggregate;
using Contracts.Domain.ProductUnitAggregate;
using Contracts.Domain.TaxAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using Polly;

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
    private readonly IReadOnlyRepository<Employee> _employeeRepository;
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
        IReadOnlyRepository<Employee> employeeRepository,
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
        _employeeRepository = employeeRepository;
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
        if (await _contractRepository.AnyAsync())
        {
            return;
        }
        
        var policy = Policy.Handle<Exception>()
            .WaitAndRetry(10, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (ex, time) =>
                {
                    _logger.LogWarning(ex, "Couldn't seed contract table after {TimeOut}s", $"{time.TotalSeconds:n1}");
                }
            );
        
        policy.Execute(() =>
        {
            if (!_customerRepository.AnyAsync().Result)
            {
                throw new Exception("Buyer data not seeded yet!");
            }
            
            if (!_productRepository.AnyAsync().Result)
            {
                throw new Exception("Product data not seeded yet!");
            }
            
            if (!_productUnitRepository.AnyAsync().Result)
            {
                throw new Exception("Product unit data not seeded yet!");
            }

            if (!_materialRepository.AnyAsync().Result)
            {
                throw new Exception("Material data not seeded yet!");
            }

            if (_employeeRepository.CountAsync().Result < 30)
            {
                throw new Exception("Employee data not seeded yet!");
            }
        });
        
        _logger.LogInformation("Begin seeding contracts data...");
        
        var products = await _productRepository.GetAllAsync();
        var productUnits = await _productUnitRepository.GetAllAsync();
        var materials = await _materialRepository.GetAllAsync();
        var customers = await _customerRepository.GetAllAsync();
        var paymentMethods = await _paymentMethodReadOnlyRepository.GetAllAsync();
        var customerServiceEmployees = await _employeeRepository.FindListAsync(new CustomerServiceEmployeeSpecification());
        var salePersonEmployees = await _employeeRepository.FindListAsync(new SalePersonEmployeeSpecification());
        var supervisorEmployees = await _employeeRepository.FindListAsync(new SupervisorEmployeeSpecification());
        
        for (var i = 0; i < 50; i++)
        {
            try
            {
                var command = new AddContractCommand(GetContractCreateDto(productUnits, products, materials, customers, paymentMethods, customerServiceEmployees, salePersonEmployees, supervisorEmployees));
                await _mediator.Send(command);
            }
            catch(Exception e)
            {
                _logger.LogError("Seeding a contract failed: {Message}", e.Message);
            }
        }
        
        _logger.LogInformation("Finished seeding contracts data...");
    }

    private ContractCreateDto GetContractCreateDto(
        IList<ProductUnit> productUnits,
        IList<Product> products,
        IList<Material> materials,
        IList<Customer> customers,
        IList<PaymentMethod> paymentMethods,
        IList<Employee> customerServiceEmployees,
        IList<Employee> salePersonEmployees,
        IList<Employee> supervisorEmployees)
    {
        var faker = new Faker();
        var customer = customers[faker.Random.Int(0, (int)(customers.Count * 0.25))];
                    
        var contractCreateDto = new ContractCreateDto()
        {
            CustomerId = customer.Id,
            CustomerNote = faker.Lorem.Sentence(),
            SalePersonId = faker.PickRandom(salePersonEmployees).Id,
            SupervisorId = faker.PickRandom(supervisorEmployees).Id,
            CustomerServiceRepId = faker.PickRandom(customerServiceEmployees).Id,
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

            var estimatedStartTime = faker.Date.Soon(40);
            var estimatedFinishTime = faker.Date.Soon(40);
            contractCreateDto.Installations.Add(new InstallationCreateDto
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Color = product.Colors[0],
                InstallDate = faker.Date.Soon(40),
                EstimatedStartTime = estimatedStartTime,
                EstimatedFinishTime = estimatedStartTime.AddHours(faker.Random.Int(0, 3)),
                ActualStartTime = estimatedFinishTime,
                ActualFinishTime = estimatedFinishTime.AddHours(faker.Random.Int(0, 3)),
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
                ActionByEmployeeId = faker.PickRandom(customerServiceEmployees).Id
            });
        }

        return contractCreateDto;
    }

    private async Task SeedTaxesAsync()
    {
        if (!await _taxRepository.AnyAsync())
        {
            _logger.LogInformation("Begin seeding taxes data...");

            var taxes = new List<Tax>()
            {
                new("5%", 0.05),
                new("10%", 0.1),
                new("15%", 0.15),
                new("20%", 0.2)
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