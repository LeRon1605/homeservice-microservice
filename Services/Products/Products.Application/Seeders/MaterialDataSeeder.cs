using Bogus;
using BuildingBlocks.Application.Seeder;
using BuildingBlocks.Domain.Data;
using Microsoft.Extensions.Logging;
using Products.Domain.MaterialAggregate;
using Products.Domain.ProductTypeAggregate;
using Products.Domain.ProductUnitAggregate;

namespace Products.Application.Seeders;

public class MaterialDataSeeder: IDataSeeder
{
    private readonly IRepository<Material> _materialRepository;
    private readonly IReadOnlyRepository<ProductType> _productTypeRepository;
    private readonly IReadOnlyRepository<ProductUnit> _productUnitRepository;
    private readonly IUnitOfWork _unitOfWork;
    private ILogger<MaterialDataSeeder> _logger;

    public MaterialDataSeeder(IRepository<Material> materialRepository, 
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
        
        SeedMaterials(productUnits.ToList(),productTypes.ToList());
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Material data seeded successfully!");
    }

    
    private void SeedMaterials(
        IReadOnlyList<ProductUnit> productUnits, 
        IReadOnlyList<ProductType> productTypes
        )
    {
        var faker = new Faker();
        var random = new Random();
        for (var i = 0; i < 9; i++)
        {
            _materialRepository.Add(new Material(
                faker.Random.AlphaNumeric(6),
                faker.Commerce.ProductMaterial(),
                productTypes[random.Next(0, productTypes.Count)].Id,
                productUnits[random.Next(0, productTypes.Count)].Id,
                faker.Random.Decimal(1, 100),
                faker.Random.Decimal(1, 100),
                faker.Random.Bool(0.1f)
            ));
        }
    }
}