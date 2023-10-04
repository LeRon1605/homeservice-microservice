using Bogus;
using BuildingBlocks.Application.Seeder;
using BuildingBlocks.Domain.Data;
using Microsoft.Extensions.Logging;
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
        if (!await _productReviewRepository.AnyAsync())
        {
            var specification = new ProductFilterSpecification(string.Empty, 1, 10);
            var products = await _productRepository.FindListAsync(specification);

            if (!products.Any())
            {
                return;
            }
            
            try
            {
                _logger.LogInformation("Begin seeding product reviews...");
                
                var faker = new Faker();
                foreach (var product in products)
                {
                    product.AddReview(faker.Commerce.ProductDescription(), faker.Random.Int(1, 10));
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
}