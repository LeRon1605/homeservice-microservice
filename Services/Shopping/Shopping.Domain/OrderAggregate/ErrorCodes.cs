namespace Shopping.Domain.OrderAggregate;

public static class ErrorCodes
{
    public const string OrderNotFound = "Order:001";
    public const string OrderProcessed = "Order:002";
    public const string ProductAlreadyAddedToOrder = "Order:003";
}