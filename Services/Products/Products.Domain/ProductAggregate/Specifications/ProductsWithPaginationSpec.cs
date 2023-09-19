using System.Linq.Expressions;
using BuildingBlocks.Domain.Specification;

namespace Products.Domain.ProductAggregate.Specifications;

public class ProductsWithPaginationSpec : Specification<Product>
{
    private readonly string? _search;
    private readonly string? _groupId;
    private readonly string? _typeId;
    private readonly bool? _isObsolete;
    
    public override Expression<Func<Product, bool>> ToExpression()
    {
        return p => (string.IsNullOrWhiteSpace(_search) 
                     || p.Name.ToLower().Contains(_search.ToLower())
                     || p.ProductCode.ToLower().Contains(_search.ToLower()))
                   && (!_isObsolete.HasValue || p.IsObsolete == _isObsolete)
                   && (string.IsNullOrWhiteSpace(_groupId) || p.ProductGroupId.ToString() == _groupId)
                   && (string.IsNullOrWhiteSpace(_typeId) || p.ProductTypeId.ToString() == _typeId);
    }

    public ProductsWithPaginationSpec(string? search, 
                                      int pageIndex, 
                                      int pageSize, 
                                      bool? isObsolete, 
                                      Guid? groupId, 
                                      Guid? typeId)
    {
        _search = search;
        _groupId = groupId.ToString();
        _typeId = typeId.ToString();
        _isObsolete = isObsolete;
        
        ApplyPaging(pageIndex, pageSize);
        AddInclude(x => x.Group);
        AddInclude(x => x.Images);
        AddInclude(x => x.BuyUnit);
        AddInclude(x => x.SellUnit);
        AddInclude(x => x.Type);
    }
}