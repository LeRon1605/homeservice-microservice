using Bogus;
using BuildingBlocks.Application.Seeder;
using BuildingBlocks.Domain.Data;
using Microsoft.Extensions.Logging;
using Polly;
using Shopping.Domain.ProductAggregate;
using Shopping.Domain.ProductAggregate.Specifications;

namespace Shopping.Application.Seeders;

public class ProductReviewDataSeeder : IDataSeeder
{
    private readonly IRepository<Product> _productRepository;
    private readonly IReadOnlyRepository<ProductReview> _productReviewRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ProductReviewDataSeeder> _logger;

    public ProductReviewDataSeeder(
        IRepository<Product> productRepository, 
        IReadOnlyRepository<ProductReview> productReviewRepository,
        IUnitOfWork unitOfWork,
        ILogger<ProductReviewDataSeeder> logger)
    {
        _productRepository = productRepository;
        _productReviewRepository = productReviewRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task SeedAsync()
    {
        if (await _productReviewRepository.AnyAsync())
        {
            return;
        }
        
        var policy = Policy.Handle<Exception>()
            .WaitAndRetry(10, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (ex, time) =>
                {
                    _logger.LogWarning(ex, "Couldn't seed product review table after {TimeOut}s", $"{time.TotalSeconds:n1}");
                }
            );

        policy.Execute(() =>
        {
            if (!_productRepository.AnyAsync().Result)
            {
                throw new Exception("Product data not seeded yet!");
            }
        });
            
        try
        {
            _logger.LogInformation("Begin seeding product reviews...");
            
            var specification = new ProductFilterSpecification(string.Empty, 1, 10);
            var products = await _productRepository.FindListAsync(specification);
            
            foreach (var product in products)
            {
                var faker = new Faker();
                for (var i = 0;i < faker.Random.Int(1, 20);i++)
                {
                    product.AddReview(faker.Commerce.ProductDescription(), faker.Random.Int(1, 10));
                }
                
                _productRepository.Update(product);
            }
            
            await _unitOfWork.SaveChangesAsync();
                
            _logger.LogInformation("Seed product reviews successfully!");
        }
        catch
        {
            _logger.LogError("Seed product reviews failed!");
        }
    }
}