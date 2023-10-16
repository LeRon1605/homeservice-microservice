using System.Text.Json.Serialization;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Dtos;
using Installations.Application.Dtos;

namespace Installations.Application.Queries;

public class GetInstallationsByContractIdQuery : PagingParameters, IQuery<PagedResult<InstallationInContractDto>>
{
    [JsonIgnore]
    public Guid ContractId { get; set; }
}