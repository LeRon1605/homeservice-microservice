using Employees.Application.Command.Employees;
using Employees.Application.Dtos;
using Employees.Application.Queries;
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
}