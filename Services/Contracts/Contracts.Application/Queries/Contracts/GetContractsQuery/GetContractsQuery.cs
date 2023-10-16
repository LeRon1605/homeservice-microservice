using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Dtos;
using Contracts.Application.Dtos.Contracts;
using Contracts.Domain.ContractAggregate;
using Contracts.Domain.ContractAggregate.Enums;

namespace Contracts.Application.Queries.Contracts.GetContractsQuery;

public class GetContractsQuery : PagingParameters, IQuery<PagedResult<ContractDto>>
{
    public List<ContractStatus>? Statuses { get; init; } 
    
    public DateTime? FromDate { get; init; }
    
    public DateTime? ToDate { get; init; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [EnumDataType(typeof(DateFilterType))]
    public DateFilterType? FilterDateType { get; init; }
    
    public Guid? SalePersonId { get; init; }
    
    public Guid? CustomerServiceRepId { get; init; }
}
