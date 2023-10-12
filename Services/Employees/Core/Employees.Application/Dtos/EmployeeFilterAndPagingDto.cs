using System.Text.Json.Serialization;
using BuildingBlocks.Application.Dtos;
using Employees.Domain.EmployeeAggregate.Enums;

namespace Employees.Application.Dtos;

public class EmployeeFilterAndPagingDto : PagingParameters
{
    [Newtonsoft.Json.JsonConverter(typeof(JsonStringEnumConverter))]
    public List<Status>? Status { get; set; }
}