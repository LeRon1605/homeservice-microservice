namespace Contracts.Application.Dtos.Contracts;

public class ContractConvertedFromOrderDto : ContractCreateDto
{
    public Guid OrderId { get; set; }
}