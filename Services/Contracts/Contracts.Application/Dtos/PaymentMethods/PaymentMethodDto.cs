namespace Contracts.Application.Dtos.PaymentMethods;

public class PaymentMethodDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}