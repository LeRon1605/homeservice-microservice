using Bogus;
using BuildingBlocks.Application.Seeder;
using BuildingBlocks.Domain.Data;
using Contracts.Application.Commands.Customers.AddCustomer;
using Contracts.Domain.CustomerAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.Seeders;

public class CustomerDataSeeder : IDataSeeder
{
    private readonly IRepository<Customer> _customerRepository;
    private readonly ILogger<CustomerDataSeeder> _logger;
    private readonly IMediator _mediator;
    
    public CustomerDataSeeder(
        IRepository<Customer> customerRepository,
        ILogger<CustomerDataSeeder> logger,
        IMediator mediator)
    {
        _customerRepository = customerRepository;
        _logger = logger;
        _mediator = mediator;
    }
    
    public async Task SeedAsync()
    {
        if (!await _customerRepository.AnyAsync())
        {
            _logger.LogInformation("Begin seeding customers data...");
            
            for (var i = 0; i < 50; i++)
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