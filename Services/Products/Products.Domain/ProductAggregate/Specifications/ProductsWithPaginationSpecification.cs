using System.Linq.Expressions;
using BuildingBlocks.Domain.Specification;

namespace Products.Domain.ProductAggregate.Specifications;

public class ProductsWithPaginationSpecification : Specification<Product>
{
    public ProductsWithPaginationSpecification(string? search, 
                                      int pageIndex, 
                                      int pageSize, 
                                      bool? isObsolete, 
                                      Guid? groupId, 
                                      Guid? typeId)
    {
        if (!string.IsNullOrWhiteSpace(search))
        {
            AddSearchTerm(search);
            AddSearchField(nameof(Product.ProductCode));
            AddSearchField(nameof(Product.Name));
        }
        
        if (isObsolete.HasValue)
            AddFilter(p => p.IsObsolete == isObsolete);
        
        if (groupId.HasValue)
            AddFilter(p => p.ProductGroupId == groupId);
        
        if (typeId.HasValue)
            AddFilter(p => p.ProductTypeId == typeId);
        
        ApplyPaging(pageIndex, pageSize);
        
        AddInclude(x => x.Group);
        AddInclude(x => x.Images);
        AddInclude(x => x.BuyUnit);
        AddInclude(x => x.SellUnit);
        AddInclude(x => x.Type);
    }
}