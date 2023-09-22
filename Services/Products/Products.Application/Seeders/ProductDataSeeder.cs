using BuildingBlocks.Application.Seeder;
using BuildingBlocks.Domain.Data;
using Microsoft.Extensions.Logging;
using Products.Domain.ProductAggregate;
using Products.Domain.ProductGroupAggregate;
using Products.Domain.ProductTypeAggregate;
using Products.Domain.ProductUnitAggregate;

namespace Products.Application.Seeders;

public class ProductDataSeeder : IDataSeeder
{
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<ProductUnit> _productUnitRepository;
    private readonly IRepository<ProductType> _productTypeRepository;
    private readonly IRepository<ProductGroup> _productGroupRepository;
    private readonly ILogger<ProductDataSeeder> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ProductDataSeeder(
        IRepository<Product> productRepository,
        IRepository<ProductUnit> productUnitRepository,
        IRepository<ProductType> productTypeRepository,
        IRepository<ProductGroup> productGroupRepository,
        ILogger<ProductDataSeeder> logger,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _productUnitRepository = productUnitRepository;
        _productTypeRepository = productTypeRepository;
        _productGroupRepository = productGroupRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    
    public async Task SeedAsync()
    {
        if (await _productRepository.AnyAsync())
        {
            _logger.LogInformation("No need to seed data!");
            return;
        }
        
        var productUnits = SeedProductUnits();
        var productTypes = SeedProductTypes();
        var productGroups = SeedProductGroups();
        await SeedProductAsync(productUnits, productTypes, productGroups);
        
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Seed data successfully!");
    }

    private List<ProductType> SeedProductTypes()
    {
        var data = new List<ProductType>()
        {
            new("Carpet"),
            new("Sofa"),
            new("Chair"),
            new("Small light")
        };

        foreach (var productType in data)
        {
            _productTypeRepository.Add(productType);
        }

        return data;
    }
    
    private List<ProductUnit> SeedProductUnits()
    {
        var data = new List<ProductUnit>()
        {
            new("Roll"),
            new("Metre"),
            new("Item"),
            new("Box")
        };

        foreach (var productUnit in data)
        {
            _productUnitRepository.Add(productUnit);
        }

        return data;
    }
    
    private List<ProductGroup> SeedProductGroups()
    {
        var data = new List<ProductGroup>()
        {
            new("Roll"),
            new("Metre"),
            new("Item"),
            new("Box")
        };

        foreach (var productGroup in data)
        {
            _productGroupRepository.Add(productGroup);
        }

        return data;
    }

    private async Task SeedProductAsync(
        IReadOnlyList<ProductUnit> productUnits, 
        IReadOnlyList<ProductType> productTypes, 
        IReadOnlyList<ProductGroup> productGroups)
    {
        var random = new Random();

        for (var i = 0; i < 10; i++)
        {
            var product = await Product.InitAsync(
                $"PROD-{i}",
                $"Product - {i}",
                productTypes[random.Next(0, productTypes.Count)].Id,
                productGroups[random.Next(0, productTypes.Count)].Id,
                string.Empty,
                random.Next(0, 1) == 0,
                productUnits[random.Next(0, productTypes.Count)].Id,
                300,
                productUnits[random.Next(0, productTypes.Count)].Id,
                500,
                Array.Empty<string>(),
                _productRepository
            );
            
            product.AddImage("http://cb.dut.udn.vn/ImageSV/20/102200109.jpg");
            
            _productRepository.Add(product);
        }
    }
}