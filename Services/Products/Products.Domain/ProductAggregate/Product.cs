using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Data;
using BuildingBlocks.Domain.Models;
using Products.Domain.ProductAggregate.Exceptions;
using Products.Domain.ProductAggregate.Specifications;
using Products.Domain.ProductGroupAggregate;
using Products.Domain.ProductTypeAggregate;
using Products.Domain.ProductUnitAggregate;

namespace Products.Domain.ProductAggregate;

public class Product : AggregateRoot
{
    public string ProductCode { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public bool IsObsolete { get; private set; }
    
    public Guid? BuyUnitId { get; private set; }
    public ProductUnit BuyUnit { get; private set; } = null!;
    
    public decimal? BuyPrice { get; private set; }
    
    public Guid? SellUnitId { get; private set; }
    public ProductUnit SellUnit { get; private set; } = null!;
    
    public decimal SellPrice { get; private set; }
    
    public Guid ProductTypeId { get; private set; }
    public ProductType Type { get; private set; } = null!;
    
    public Guid ProductGroupId { get; private set; }
    public ProductGroup Group { get; private set; } = null!;
    
    public List<ProductImage> Images { get; private set; }
        
    public Product(
        string productCode,
        string name,
        Guid productTypeId, 
        Guid productGroupId,
        string? description = null,
        bool isObsolete = false,
        Guid? buyUnitId = null,
        decimal? buyPrice = null,
        Guid? sellUnitId = null,
        decimal sellPrice = 0)
    {
        ProductCode = Guard.Against.NullOrWhiteSpace(productCode, nameof(ProductCode));
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(Name));
        ProductTypeId = Guard.Against.Null(productTypeId, nameof(ProductTypeId));
        ProductGroupId = Guard.Against.Null(productGroupId, nameof(ProductGroupId));
        IsObsolete = isObsolete;
        Description = description;
        BuyUnitId = buyUnitId;
        BuyPrice = buyPrice;
        SellUnitId = sellUnitId;
        SellPrice = sellPrice;
        Images = new List<ProductImage>();
    }

    public void AddImage(string url)
    {
        if (HasImage(url))
        {
            throw new ProductImageDuplicateException(url);
        }
        
        Images.Add(new ProductImage(url, Id));
    }

    public void RemoveImage(Guid id)
    {
        var imageToRemove = Images.FirstOrDefault(x => x.Id == id);

        if (imageToRemove == null)
        {
            throw new ProductImageNotFoundException(id);
        }
        
        Images.Remove(imageToRemove);
    }

    private bool HasImage(string url)
    {
        return Images.Any(x => x.Url == url);
    }

    public static async Task<Product> InitAsync(
            string productCode,
            string name,
            Guid productTypeId, 
            Guid productGroupId,
            string? description ,
            bool isObsolete ,
            Guid? buyUnitId,
            decimal? buyPrice,
            Guid? sellUnitId,
            decimal sellPrice,
            string[] urls,
            IRepository<Product> productRepository)
    {
        if (await productRepository.AnyAsync(new IsProductCodeExistsSpecification(productCode)))
            throw new DuplicateProductCodeException("Code is existing");
        var product = new Product(productCode, name, productTypeId, productGroupId, description, isObsolete, buyUnitId,
            buyPrice, sellUnitId, sellPrice);
        
        foreach (var url in urls)
        {
            product.AddImage(url);
        }

        return product;
    }

    public async Task UpdateAsync(
        string productCode,
        string name,
        Guid productTypeId, 
        Guid productGroupId,
        string? description ,
        bool isObsolete ,
        Guid? buyUnitId,
        decimal? buyPrice,
        Guid? sellUnitId,
        decimal sellPrice,
        string[] urls,
        IRepository<Product> productRepository)
    {

        await SetCodeAsync(productCode, productRepository);
        Name = name;
        ProductTypeId = productTypeId;
        ProductGroupId = productGroupId;
        Description = description;
        IsObsolete = isObsolete;
        BuyUnitId = buyUnitId;
        BuyPrice = buyPrice;
        SellUnitId = sellUnitId;
        SellPrice = sellPrice;
        var images = new List<ProductImage>();
        
        urls.ToList().ForEach(url => images.Add(new ProductImage(url, Id)));
        Images.Clear();
        Images.AddRange(images);
    }

    private async Task SetCodeAsync(string productCode, IRepository<Product> productRepository)
    {
        if (ProductCode != productCode)
        {
            if (await productRepository.AnyAsync(new IsProductCodeExistsSpecification(productCode)))
                throw new DuplicateProductCodeException("Code is existing");
            ProductCode = productCode;
        }
    }
}