using Bogus;
using BuildingBlocks.Application.Seeder;
using BuildingBlocks.Domain.Data;
using Customers.Domain.CustomerAggregate;
using Microsoft.Extensions.Logging;

namespace Customers.Application.Seeders;

public class CustomerSeeder : IDataSeeder
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CustomerSeeder> _logger;

    public CustomerSeeder(
        ICustomerRepository customerRepository, 
        IUnitOfWork unitOfWork,
        ILogger<CustomerSeeder> logger)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task SeedAsync()
    {
        if (await _customerRepository.AnyAsync())
        {
            _logger.LogInformation("Customer data already seeded!");
            return;
        }
        
        SeedCustomers();
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Customer data seeded successfully!");
    }

    private void SeedCustomers()
    {
        var faker = new Faker();

        for (var i = 0; i < 10; i++)
        {
            _customerRepository.Add(new Customer(
                faker.Company.CompanyName(),
                faker.Name.FullName(),
                faker.Internet.Email(),
                faker.Address.FullAddress(),
                faker.Address.City(),
                faker.Address.State(),
                faker.Address.ZipCode(),
                faker.Phone.PhoneNumber()));    
        }
    }
}