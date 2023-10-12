using Bogus;
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

        await Task.Delay(TimeSpan.FromSeconds(30));
        
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
            new("Kitchen and Appliances"),
            new("Living room"),
            new("Lighting"),
            new("Accessories")
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
        var images = new string[]
        {
            "https://res.cloudinary.com/dboijruhe/image/upload/v1695951716/HomeService/tbt25tc5hu19mtkk2jlv.png",
            "https://res.cloudinary.com/dboijruhe/image/upload/v1695951696/HomeService/n88xfzo3rufft2ljknbt.jpg",
            "https://res.cloudinary.com/dboijruhe/image/upload/v1697075216/HomeService/sfdt5llrc8vvddbnvuyd.png",
            "https://res.cloudinary.com/dboijruhe/image/upload/v1697075200/HomeService/uwky30nwy0fodnzufayc.png",
            "https://res.cloudinary.com/dboijruhe/image/upload/v1697075183/HomeService/gxs5wymkwrtulaxixsxa.png"
        };
        
        for (var i = 0; i < 100; i++)
        {
            var faker = new Faker();
            var random = new Random();
            
            var product = await Product.InitAsync(
                $"PO - {i}",
                faker.Commerce.ProductName(),
                productTypes[random.Next(0, productTypes.Count)].Id,
                productGroups[random.Next(0, productTypes.Count)].Id,
                faker.Commerce.ProductDescription(),
                random.Next(0, 1) == 0,
                productUnits[random.Next(0, productTypes.Count - 1)].Id,
                decimal.Parse(faker.Commerce.Price()),
                productUnits[random.Next(0, productTypes.Count - 1)].Id,
                decimal.Parse(faker.Commerce.Price()),
                Array.Empty<string>(),
                new [] { 
                    System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(faker.Commerce.Color()),
                    System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(faker.Commerce.Color()),
                    System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(faker.Commerce.Color())
                }, 
                _productRepository
            );

            try
            {
                for (var j = 0; j < 2; j++)
                {
                    product.AddImage(images[random.Next(0, images.Length - 1)]);
                }
            }
            catch 
            {
                // ignored
            }

            _productRepository.Add(product);
        }
    }
}