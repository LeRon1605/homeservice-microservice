using BuildingBlocks.Application.Seeder;
using BuildingBlocks.Domain.Data;
using Employees.Domain.EmployeeAggregate;
using Bogus;
using Employees.Domain.EmployeeAggregate.Enums;
using Employees.Domain.RoleAggregate;
using Microsoft.Extensions.Logging;
using Polly;

namespace Employees.Application.Seeder;

public class EmployeeDataSeeder : IDataSeeder
{
    private readonly IRepository<Employee> _employeeRepository;
    private readonly IReadOnlyRepository<Role> _roleRepository;
    private readonly ILogger<EmployeeDataSeeder> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeDataSeeder(IRepository<Employee> employeeRepository,
        IReadOnlyRepository<Role> roleRepository, ILogger<EmployeeDataSeeder> logger, IUnitOfWork unitOfWork)
    {
        _employeeRepository = employeeRepository;
        _roleRepository = roleRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task SeedAsync()
    {
        if (await _employeeRepository.AnyAsync())
        {
            _logger.LogInformation("No need to seed employee data!");
            return;
        }
        
        var policy = Policy.Handle<Exception>()
            .WaitAndRetry(10, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (ex, time) =>
                {
                    _logger.LogWarning(ex, "Couldn't seed employee table after {TimeOut}s", $"{time.TotalSeconds:n1}");
                }
            );
        
        policy.Execute(() =>
        {
            if (!_roleRepository.AnyAsync().Result)
            {
                throw new Exception("Role data not seeded yet!");
            }
        });

        var roles = await _roleRepository.GetAllAsync();
        IList<Role> roleEmployees = roles.Where(roleEmployee => roleEmployee.Name != "Admin").ToList();

        try
        {
            var faker = new Faker();
            for (int i = 0; i < 49; i++)
            {
                var role = roles[faker.Random.Int(0, roles.Count - 1)];
                var employee = new Employee(
                    i+1000,
                    faker.Name.Random.String2(10),
                    faker.Company.Random.String2(10),
                    faker.Internet.Email(),
                    role.Id,
                    role.Name,
                    faker.Random.Int(12029302, 939493392).ToString(),
                    faker.Random.Enum<Status>()
                    );
                _employeeRepository.Add(employee);
            }

            await _unitOfWork.SaveChangesAsync();
            _logger.LogTrace("Seed employee data successfully!");
        }
        catch (Exception e)
        {
            _logger.LogTrace("Seed employee data failed!");
        }
    }
}