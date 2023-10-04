namespace Contracts.Domain.CustomerAggregate;

public static class ErrorCodes
{
    public const string CustomerNotFound = "Customer:001";
    public const string CustomerEmailAlreadyExist = "Customer:002";
    public const string CustomerPhoneAlreadyExist = "Customer:003";
    public const string DeletedCustomerHasContract = "Customer:004";
}