namespace Shopping.Domain;

public class ErrorCodes
{
    public const string OrderNotFound = "Order:001";
    public const string OrderStatusInvalid = "Order:002";
    
    public const string CustomerNotFound = "Customer:001";
    public const string CustomerEmailAlreadyExist = "Customer:002";
    public const string CustomerPhoneAlreadyExist = "Customer:003";
}