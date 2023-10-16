using BuildingBlocks.Application.CQRS;
using Contracts.Application.Dtos.Contracts;

namespace Contracts.Application.Queries.Contracts.GetContractsOfCustomer;

public class ContractsOfCustomerQuery : IQuery<IEnumerable<ContractsOfCustomerDto>>
{
    public Guid CustomerId { get; set; }
    public string? ContractNo { get; set; }

    public ContractsOfCustomerQuery(Guid customerId, ContractOfCustomerFilterDto contractOfCustomerFilterDto)
    {
        CustomerId = customerId;
        ContractNo = contractOfCustomerFilterDto.ContractNo;
    }
}