using BuildingBlocks.Domain.Specification;
using Employees.Domain.EmployeeAggregate.Enums;
using Employees.Domain.RoleAggregate;

namespace Employees.Domain.EmployeeAggregate.Specifications;

public class EmployeeFilterAndPagingSpecification : Specification<Employee>
{
    public EmployeeFilterAndPagingSpecification(string? search, List<Status>? status,
        int pageIndex,
        int pageSize)
    {
        AddInclude(x => x.Role);
        if (!string.IsNullOrWhiteSpace(search))
        {
            //AddSearchTerm(search);
            AddFilter(x => x.EmployeeCode.ToString().Contains(search)
                           || x.FullName.Contains(search) || x.Email.Contains(search)
                           || (x.Phone != null && x.Phone.Contains(search)) || x.Position.Contains(search) ||
                           x.Role.Name.Contains(search));
        }

        if (status != null)
        {
            AddFilter(x => status.Contains(x.Status));
        }

        ApplyPaging(pageIndex, pageSize);
    }
}