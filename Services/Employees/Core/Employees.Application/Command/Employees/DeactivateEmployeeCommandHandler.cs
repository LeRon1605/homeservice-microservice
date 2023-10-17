using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Employees.Application.Dtos;
using Employees.Domain.EmployeeAggregate;
using Employees.Domain.Exceptions;

namespace Employees.Application.Command.Employees;
 
public class DeactivateEmployeeCommandHandler : ICommandHandler<DeactivateEmployeeCommand, GetEmployeesDto>
{
    private readonly IRepository<Employee> _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public DeactivateEmployeeCommandHandler(IRepository<Employee> employeeRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetEmployeesDto> Handle(DeactivateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(request.Id);
        if (employee == null)
        {
            throw new EmployeeNotFoundException(request.Id);
        }
        
        employee.Deactivate();
        
        _employeeRepository.Update(employee);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<GetEmployeesDto>(employee);
    }
}