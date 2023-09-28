using BuildingBlocks.Domain.Specification;

namespace Shopping.Domain.BuyerAggregate.Specifications;

public class BuyerFilterSpecification : Specification<Buyer>
{
    public BuyerFilterSpecification(string? search, int pageIndex, int pageSize)
    {
        ApplyPaging(pageIndex, pageSize);

        if (!string.IsNullOrWhiteSpace(search))
        {
            AddSearchTerm(search);
            AddSearchField(nameof(Buyer.FullName));
            AddSearchField(nameof(Buyer.Email));
            AddSearchField(nameof(Buyer.Phone));
        }
    }
}