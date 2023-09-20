using Bogus;
using BuildingBlocks.Application.Seeder;
using BuildingBlocks.Domain.Data;
using Microsoft.Extensions.Logging;
using Products.Domain.MaterialAggregate;
using Products.Domain.MaterialAggregate.DomainServices;
using Products.Domain.ProductTypeAggregate;
using Products.Domain.ProductUnitAggregate;

namespace Products.Application.Seeders;

public class MaterialDataSeeder: IDataSeeder
{
    private readonly IRepository<Material> _materialRepository;
    private readonly IReadOnlyRepository<ProductType> _productTypeRepository;
    private readonly IReadOnlyRepository<ProductUnit> _productUnitRepository;
    private readonly IMaterialDomainService _materialDomainService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<MaterialDataSeeder> _logger;

    public MaterialDataSeeder(IRepository<Material> materialRepository, 
        IMaterialDomainService materialDomainService,
        IUnitOfWork unitOfWork,
        ILogger<MaterialDataSeeder> logger, 
        IReadOnlyRepository<ProductType> productTypeRepository,
        IReadOnlyRepository<ProductUnit> productUnitRepository)
    {
        _materialRepository = materialRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _productTypeRepository = productTypeRepository;
        _productUnitRepository = productUnitRepository;
        _materialDomainService = materialDomainService;
    }

    public async Task SeedAsync()
    {
        if (await _materialRepository.AnyAsync())
        {
            _logger.LogInformation("Material data already seeded!");
            return;
        }

        var productUnits = await _productUnitRepository.GetAllAsync();
        var productTypes = await _productTypeRepository.GetAllAsync();
        
        await SeedMaterialsAsync(productUnits.ToList(),productTypes.ToList());
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Material data seeded successfully!");
    }

    
    private async Task SeedMaterialsAsync(
        IReadOnlyList<ProductUnit> productUnits, 
        IReadOnlyList<ProductType> productTypes)
    {
        var faker = new Faker();
        var random = new Random();
        for (var i = 0; i < 9; i++)
        {
            var material = await _materialDomainService.CreateAsync(
                faker.Random.AlphaNumeric(6),
                faker.Commerce.ProductMaterial(),
                productTypes[random.Next(0, productTypes.Count)].Id,
                productUnits[random.Next(0, productTypes.Count)].Id,
                faker.Random.Decimal(1, 100),
                faker.Random.Decimal(1, 100),
                faker.Random.Bool(0.1f));
            _materialRepository.Add(material);
        }
    }
}