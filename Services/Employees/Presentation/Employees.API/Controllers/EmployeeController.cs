using BuildingBlocks.Application.Dtos;
using Employees.Application.Command.Employees;
using Employees.Application.Dtos;
using Employees.Application.Queries;
using Employees.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employees.API.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;
    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin, Sales person, Customer service, Installer, Supervisor")]
    [ProducesResponseType(typeof(PagedResult<GetEmployeesDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEmployees([FromQuery] EmployeeFilterAndPagingDto employeeFilterAndPagingDto)
    {
        var employees = await _mediator.Send(new GetEmployeesWithPaginationQuery(employeeFilterAndPagingDto));
        return Ok(employees);
    }
    
    [HttpGet("installers")]
    [Authorize(Roles = "Admin, Sales person, Customer service, Installer, Supervisor")]
    [ProducesResponseType(typeof(IEnumerable<GetEmployeesDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetInstallers()
    {
        var installers = await _mediator.Send(new GetAllEmployeeByRoleQuery(AppRole.Installer));
        return Ok(installers);
    }
    
    [HttpGet("supervisors")]
    [Authorize(Roles = "Admin, Sales person, Customer service, Installer, Supervisor")]
    [ProducesResponseType(typeof(IEnumerable<GetEmployeesDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSupervisors()
    {
        var supervisors = await _mediator.Send(new GetAllEmployeeByRoleQuery(AppRole.Supervisor));
        return Ok(supervisors);
    }
    
    [HttpGet("salespersons")]
    [Authorize(Roles = "Admin, Sales person, Customer service, Installer, Supervisor")]
    [ProducesResponseType(typeof(IEnumerable<GetEmployeesDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSalesPersons()
    {
        var salesPersons = await _mediator.Send(new GetAllEmployeeByRoleQuery(AppRole.SalesPerson));
        return Ok(salesPersons);
    }
    
    [HttpGet("customer-service")]
    [Authorize(Roles = "Admin, Sales person, Customer service, Installer, Supervisor")]
    [ProducesResponseType(typeof(IEnumerable<GetEmployeesDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomerServiceEmployees()
    {
        var salesPersons = await _mediator.Send(new GetAllEmployeeByRoleQuery(AppRole.CustomerService));
        return Ok(salesPersons);
    }
    
    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin, Sales person, Customer service, Installer, Supervisor")]
    [ProducesResponseType(typeof(GetEmployeesDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEmployeeById(Guid id)
    {
        var employee = await _mediator.Send(new GetEmployeeByIdQuery(id));
        return Ok(employee);
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin, Sales person, Customer service, Installer, Supervisor")]
    [ProducesResponseType(typeof(GetEmployeesDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateEmployee([FromBody]CreateEmployeeDto createEmployeeDto)
    {
        var employee = await _mediator.Send(new CreateEmployeeCommand(createEmployeeDto));
        return CreatedAtAction(nameof(GetEmployeeById), new {id = employee.Id}, employee);
    }
    
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin, Sales person, Customer service, Installer, Supervisor")]
    [ProducesResponseType(typeof(GetEmployeesDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody]UpdateEmployeeDto updateEmployeeDto)
    {
        var employee = await _mediator.Send(new UpdateEmployeeCommand(id, updateEmployeeDto));
        return Ok(employee);
    }
    
    [HttpPost("{id:guid}/deactivate")]
    [Authorize(Roles = "Admin, Sales person, Customer service, Installer, Supervisor")]
    [ProducesResponseType(typeof(GetEmployeesDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeactivateEmployee(Guid id)
    {
        var employee = await _mediator.Send(new DeactivateEmployeeCommand(id));
        return Ok(employee);
    }
}