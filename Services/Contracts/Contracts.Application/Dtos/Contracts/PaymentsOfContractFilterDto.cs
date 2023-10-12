using BuildingBlocks.Application.Dtos;

namespace Contracts.Application.Dtos.Contracts;

public class PaymentsOfContractFilterDto : PagingParameters
{
    public bool IsShowDeleted { get; set; }
}