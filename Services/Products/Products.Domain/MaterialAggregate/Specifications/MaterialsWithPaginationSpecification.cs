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
        if (!string.IsNullOrWhiteSpace(search))
        {
            AddSearchTerm(search);
            AddSearchField(nameof(Material.Name));
            AddSearchField(nameof(Material.MaterialCode));
        }
        
        if (isObsolete.HasValue)
            AddFilter(p => p.IsObsolete == isObsolete);
        
        if (typeId.HasValue)
            AddFilter(p => p.ProductTypeId == typeId);
        
        ApplyPaging(pageIndex,pageSize);
        AddInclude(x => x.Type);
    }
}