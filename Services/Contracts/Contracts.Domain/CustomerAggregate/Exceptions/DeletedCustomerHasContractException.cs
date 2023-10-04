using BuildingBlocks.Domain.Exceptions.Resource;

namespace Contracts.Domain.CustomerAggregate.Exceptions;

public class DeletedCustomerHasContractException : ResourceAlreadyExistException
{
    public DeletedCustomerHasContractException(Guid customerId) : base(
        $"Can not delete customer with id: {customerId} already has contracts")
    {
    }
}