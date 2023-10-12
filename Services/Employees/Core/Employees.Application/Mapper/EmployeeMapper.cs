using AutoMapper;
using Employees.Application.Dtos;
using Employees.Domain.EmployeeAggregate;

namespace Employees.Application.Mapper;

public class EmployeeMapper : Profile
{
    public EmployeeMapper()
    {
        CreateMap<Employee, GetEmployeesDto>();
    }
}