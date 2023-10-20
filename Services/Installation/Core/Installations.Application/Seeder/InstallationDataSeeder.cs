using Bogus;
using BuildingBlocks.Application.Seeder;
using BuildingBlocks.Domain.Data;
using Installations.Application.Commands;
using Installations.Application.Dtos;
using Installations.Domain.ContractAggregate;
using Installations.Domain.ContractAggregate.Specifications;
using Installations.Domain.InstallationAggregate;
using Installations.Domain.InstallerAggregate;
using Installations.Domain.MaterialAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using Polly;

namespace Installations.Application.Seeder;

public class InstallationDataSeeder : IDataSeeder
{
    private readonly IReadOnlyRepository<Contract> _contractRepository;
    private readonly IReadOnlyRepository<Installation> _installationRepository;
    private readonly IReadOnlyRepository<Material> _materialRepository;
    private readonly IReadOnlyRepository<Installer> _installerRepository;
    private readonly ILogger<InstallationDataSeeder> _logger;
    private readonly IMediator _mediator;

    public InstallationDataSeeder(IReadOnlyRepository<Contract> contractRepository,
                                  IReadOnlyRepository<Installation> installationRepository,
                                  ILogger<InstallationDataSeeder> logger,
                                  IReadOnlyRepository<Installer> installerRepository,
                                  IReadOnlyRepository<Material> materialRepository,
                                  IMediator mediator)
    {
        _contractRepository = contractRepository;
        _installationRepository = installationRepository;
        _logger = logger;
        _installerRepository = installerRepository;
        _materialRepository = materialRepository;
        _mediator = mediator;
    }

    public async Task SeedAsync()
    {
        if (await _installationRepository.AnyAsync()) return;
        
        var policy = Policy.Handle<Exception>()
            .WaitAndRetry(10, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (ex, time) =>
                {
                    _logger.LogWarning(ex, "Couldn't seed employee table after {TimeOut}s", $"{time.TotalSeconds:n1}");
                }
            );
        
        policy.Execute(() =>
        {
            if (!_contractRepository.AnyAsync().Result)
            {
                throw new Exception("Contract data not seeded yet!");
            }

            if (!_materialRepository.AnyAsync().Result)
            {
                throw new Exception("Material data not seeded yet!");
            }
        });

        var contracts = await _contractRepository.FindListAsync(new GetContractsWithContractLineSpecification());
        var installers = await _installerRepository.GetAllAsync();
        var materials = await _materialRepository.GetAllAsync();
        
        foreach (var contract in contracts)
        {
            foreach (var contractLine in contract.ContractLines)
            {
                var faker = new Faker();
                var estimatedStartTime = faker.Date.Soon(40);
                var estimatedFinishTime = faker.Date.Soon(40);
                var material = materials[faker.Random.Int(0, materials.Count - 1)];
                var command = new AddInstallationCommand
                {
                    ContractId = contract.Id,
                    ContractLineId = contractLine.Id,
                    InstallerId = installers[faker.Random.Int(0, installers.Count - 1)].Id, 
                    InstallDate = faker.Date.Soon(40),
                    EstimatedStartTime = estimatedStartTime,
                    EstimatedFinishTime = estimatedStartTime.AddHours(faker.Random.Int(0, 3)),
                    ActualStartTime = estimatedFinishTime,
                    ActualFinishTime = estimatedFinishTime.AddHours(faker.Random.Int(0, 3)),
                    InstallationComment = faker.Lorem.Sentence(),
                    FloorType = faker.Random.ListItem(new List<string> {"Concrete", "Wood", "Tile"}),
                    InstallationMetres = faker.Random.Double(10, 100),
                    InstallationItems = new List<InstallationItemCreateDto>
                    {
                        new()
                        {
                            MaterialId = material.Id,
                            Quantity = faker.Random.Int(1, 10),
                        } 
                    }
                };
                
                await _mediator.Send(command);
            }
        }
    }
}