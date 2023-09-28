using BuildingBlocks.Domain.Specification;

namespace Shopping.Domain.BuyerAggregate.Specifications;

public class BuyerPhoneAlreadyExistSpecification : Specification<Buyer>
{
    public BuyerPhoneAlreadyExistSpecification(string? phone, Guid? buyerId = null)
    {
        if (!string.IsNullOrWhiteSpace(phone))
            AddFilter(buyer => buyer.Phone == phone);
        
        if (buyerId.HasValue)
            AddFilter(buyer => buyer.Id != buyerId.Value);
    } 
}