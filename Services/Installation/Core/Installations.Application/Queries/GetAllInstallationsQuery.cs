using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Dtos;
using Installations.Application.Dtos;
using Installations.Domain.InstallationAggregate.Enums;

namespace Installations.Application.Queries;

public class GetAllInstallationsQuery : PagingParameters, IQuery<PagedResult<InstallationDto>>
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [EnumDataType(typeof(InstallationStatus))]
    public InstallationStatus? Status { get; set; }
    
    public DateTime? FromInstallDate { get; set; }
    public DateTime? ToInstallDate { get; set; }
    
    public Guid? ProductId { get; set; }
    public Guid? InstallerId { get; set; }
}