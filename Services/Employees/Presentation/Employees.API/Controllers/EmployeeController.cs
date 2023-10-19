using Employees.Application.Command.Employees;
using Employees.Application.Dtos;
using Employees.Application.Queries;
using Employees.Domain.Constants;
using MediatR;
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
    public async Task<IActionResult> GetEmployees([FromQuery] EmployeeFilterAndPagingDto employeeFilterAndPagingDto)
    {
        var employees = await _mediator.Send(new GetEmployeesWithPaginationQuery(employeeFilterAndPagingDto));
        return Ok(employees);
    }
    
    [HttpGet("installers")]
    public async Task<IActionResult> GetInstallers()
    {
        var installers = await _mediator.Send(new GetAllEmployeeByRoleQuery(AppRole.Installer));
        return Ok(installers);
    }
    
    [HttpGet("supervisors")]
    public async Task<IActionResult> GetSupervisors()
    {
        var supervisors = await _mediator.Send(new GetAllEmployeeByRoleQuery(AppRole.Supervisor));
        return Ok(supervisors);
    }
    
    [HttpGet("salespersons")]
    public async Task<IActionResult> GetSalesPersons()
    {
        var salesPersons = await _mediator.Send(new GetAllEmployeeByRoleQuery(AppRole.SalesPerson));
        return Ok(salesPersons);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEmployeeById(Guid id)
    {
        var employee = await _mediator.Send(new GetEmployeeByIdQuery(id));
        return Ok(employee);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody]CreateEmployeeDto createEmployeeDto)
    {
        var employee = await _mediator.Send(new CreateEmployeeCommand(createEmployeeDto));
        return CreatedAtAction(nameof(GetEmployeeById), new {id = employee.Id}, employee);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody]UpdateEmployeeDto updateEmployeeDto)
    {
        var employee = await _mediator.Send(new UpdateEmployeeCommand(id, updateEmployeeDto));
        return Ok(employee);
    }
    
    [HttpPost("{id:guid}/deactivate")]
    public async Task<IActionResult> DeactivateEmployee(Guid id)
    {
        var employee = await _mediator.Send(new DeactivateEmployeeCommand(id));
        return Ok(employee);
    }
}