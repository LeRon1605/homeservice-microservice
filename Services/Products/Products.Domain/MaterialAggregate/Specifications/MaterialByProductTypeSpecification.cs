using BuildingBlocks.Domain.Specification;

namespace Products.Domain.MaterialAggregate.Specifications;

public class MaterialByProductTypeSpecification : Specification<Material>
{
    public MaterialByProductTypeSpecification(Guid productType, string? search, int pageIndex, int pageSize)
    {
        AddInclude(x => x.Type);
        AddInclude(x => x.SellUnit);
        
        ApplyPaging(pageIndex, pageSize);
        if (!string.IsNullOrWhiteSpace(search))
        {
            AddSearchField(nameof(Material.Name));
            AddSearchField(nameof(Material.MaterialCode));
            AddSearchTerm(search);
        }
        
        AddFilter(x => x.ProductTypeId == productType);
    }
}