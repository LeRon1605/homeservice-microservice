﻿using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Data;
using BuildingBlocks.Domain.Event;
using Contracts.Domain.ContractAggregate.Events;
using Contracts.Domain.EmployeeAggregate;

namespace Contracts.Application.DomainEventHandlers.Contracts;

public class ContractPaymentUpdatedDomainEventHandler : IDomainEventHandler<ContractPaymentUpdatedDomainEvent>
{
    private readonly ICurrentUser _currentUser;
    private readonly IReadOnlyRepository<Employee> _employeeRepository;
    
    public ContractPaymentUpdatedDomainEventHandler(
        ICurrentUser currentUser,
        IReadOnlyRepository<Employee> employeeRepository)
    {
        _currentUser = currentUser;
        _employeeRepository = employeeRepository;
    }
    
    public async Task Handle(ContractPaymentUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        if (_currentUser.IsAuthenticated && await _employeeRepository.AnyAsync(Guid.Parse(_currentUser.Id!)))
        {
            notification.Contract.AddAction(
                $"Payment updated in contract.",
                DateTime.Now,
                "System tracking",
                Guid.Parse(_currentUser.Id!));
        }
    }
}