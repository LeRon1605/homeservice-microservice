using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Data;
using BuildingBlocks.Domain.Event;
using Contracts.Domain.ContractAggregate.Events;
using Contracts.Domain.EmployeeAggregate;

namespace Contracts.Application.DomainEventHandlers.Contracts;

public class ContractPaymentDeletedDomainEventHandler : IDomainEventHandler<ContractPaymentDeletedDomainEvent>
{
    private readonly ICurrentUser _currentUser;
    private readonly IReadOnlyRepository<Employee> _employeeRepository;
    
    public ContractPaymentDeletedDomainEventHandler(
        ICurrentUser currentUser,
        IReadOnlyRepository<Employee> employeeRepository)
    {
        _currentUser = currentUser;
        _employeeRepository = employeeRepository;
    }
    
    public async Task Handle(ContractPaymentDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        if (_currentUser.IsAuthenticated && await _employeeRepository.AnyAsync(Guid.Parse(_currentUser.Id!)))
        {
            notification.Contract.AddAction(
                $"Payment deleted from contract.",
                DateTime.Now,
                "System tracking",
                Guid.Parse(_currentUser.Id!));
        }
    }
}