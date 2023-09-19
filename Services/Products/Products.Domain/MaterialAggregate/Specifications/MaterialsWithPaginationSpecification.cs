using System.Linq.Expressions;
using BuildingBlocks.Domain.Specification;

namespace Products.Domain.MaterialAggregate.Specifications;

public class MaterialsWithPaginationSpecification : Specification<Material>
{
    public MaterialsWithPaginationSpecification(string? search, 
        int pageIndex, 
        int pageSize, 
        bool? isObsolete, 
        Guid? typeId)
    {
        _search = search;
        _typeId = typeId.ToString();
        _isObsolete = isObsolete;
        
        ApplyPaging(pageIndex,pageSize);
        AddInclude(x => x.Type);
    }

    private readonly string? _search;
    private readonly string? _typeId;
    private readonly bool? _isObsolete;
    
    public override Expression<Func<Material, bool>> ToExpression()
    {
        return p => (string.IsNullOrWhiteSpace(_search) || p.Name.ToLower().Contains(_search.ToLower()))
                    && (!_isObsolete.HasValue || p.IsObsolete == _isObsolete)
                    && (string.IsNullOrWhiteSpace(_typeId) || p.ProductTypeId.ToString() == _typeId);
    }
}