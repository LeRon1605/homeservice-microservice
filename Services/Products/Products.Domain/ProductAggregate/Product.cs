using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Models;
using Products.Domain.ProductAggregate.Exceptions;
using Products.Domain.ProductGroupAggregate;
using Products.Domain.ProductTypeAggregate;
using Products.Domain.ProductUnitAggregate;

namespace Products.Domain.ProductAggregate;

public class Product : AggregateRoot
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public bool IsObsolete { get; private set; }
    
    public Guid? BuyUnitId { get; private set; }
    public ProductUnit? BuyUnit { get; private set; }
    
    public decimal? BuyPrice { get; private set; }
    
    public Guid? SellUnitId { get; private set; }
    public ProductUnit? SellUnit { get; private set; }
    
    public decimal? SellPrice { get; private set; }
    
    public Guid ProductTypeId { get; private set; }
    public ProductType Type { get; private set; }
    
    public Guid ProductGroupId { get; private set; }
    public ProductGroup Group { get; private set; }
    
    public List<ProductImage> Images { get; private set; }
        
    public Product(
        string name,
        Guid productTypeId, 
        Guid productGroupId,
        string? description = null,
        bool isObsolete = false,
        Guid? buyUnitId = null,
        decimal? buyPrice = null,
        Guid? sellUnitId = null,
        decimal? sellPrice = null)
    {
        Name = Guard.Against.NullOrEmpty(name, nameof(Name));
        ProductTypeId = Guard.Against.Default(productTypeId, nameof(ProductTypeId));
        ProductGroupId = Guard.Against.Default(productGroupId, nameof(ProductGroupId));
        IsObsolete = false;
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
}