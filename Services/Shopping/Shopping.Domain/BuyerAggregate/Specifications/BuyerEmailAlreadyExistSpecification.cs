using BuildingBlocks.Domain.Specification;

namespace Shopping.Domain.BuyerAggregate.Specifications;

public class BuyerEmailAlreadyExistSpecification : Specification<Buyer>
{
    public BuyerEmailAlreadyExistSpecification(string? email, Guid? buyerId = null)
    {
        if (!string.IsNullOrWhiteSpace(email))
            AddFilter(buyer => buyer.Email == email);
        
        if (buyerId.HasValue)
            AddFilter(buyer => buyer.Id != buyerId.Value);
    } 
}