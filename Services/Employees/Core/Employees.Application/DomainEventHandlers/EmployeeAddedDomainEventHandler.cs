using System.Security.Cryptography;
using System.Text;
using BuildingBlocks.Application.MailSender;
using BuildingBlocks.Domain.Data;
using BuildingBlocks.Domain.Event;
using BuildingBlocks.EventBus.Interfaces;
using Employees.Application.IntegrationEvents.Events.Employees;
using Employees.Domain.Events;
using Employees.Domain.RoleAggregate;
using Employees.Domain.RoleAggregate.Exceptions;
using Employees.Domain.RoleAggregate.Specifications;
using Microsoft.Extensions.Logging;

namespace Employees.Application.DomainEventHandlers;

public class EmployeeAddedDomainEventHandler : IDomainEventHandler<EmployeeAddedDomainEvent>
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<EmployeeAddedDomainEventHandler> _logger;
    private readonly IEmailSender _emailSender;
    private readonly IRepository<Role> _roleRepository;

    public EmployeeAddedDomainEventHandler(IEventBus eventBus, ILogger<EmployeeAddedDomainEventHandler> logger,
        IEmailSender emailSender, IRepository<Role> roleRepository)
    {
        _eventBus = eventBus;
        _logger = logger;
        _emailSender = emailSender;
        _roleRepository = roleRepository;
    }

    public async Task Handle(EmployeeAddedDomainEvent notification, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.FindAsync(new RoleByIdSpecification(notification.Employee.RoleId));
        if (role == null)
            throw new RoleNotFoundException(notification.Employee.RoleId);
        
        var password = GetRandomPassword();
        var content = $"<p>Dear {notification.Employee.FullName},</p>\n " +
                         $"<p>We are thrilled to inform you that your new account with HomeServiceApp has been successfully created. " +
                         $"Here are your account details: </p>\n " +
                         $"<span> - Username:  {notification.Employee.Email}</span>\n " +
                         $"<span>\n - Password:  {password}</span>\n " +
                         $"<p>Please remember to keep your login credentials secure. If you ever forget your password, you can use our \"Forgot Password\" feature on our website to reset it.</p>\n " +
                         "<p>If you have any questions or concerns, please do not hesitate to contact us at <strong>homeserviceapp@gmail.com</strong>.</p>\n " +
                         "<p>Thank you .</p>\n <p>Sincerely,</p>\n <p><strong>Home Service Company</strong></p>";
        var message = new Message(new string[] { notification.Employee.Email }, "Create Account for HomeService", content);
        //var message = new Message(new string[] { "maivietquynh0605@gmail.com" }, "Create Account for HomeService",content);
        _emailSender.SendEmail(message);

        var employeeAddedIntegrationEvent = new EmployeeAddedIntegrationEvent(
            notification.Employee.Id,
            notification.Employee.FullName,
            notification.Employee.Email,
            notification.Employee.Phone,
            role.Id,
            role.Name,
            password);

        _eventBus.Publish(employeeAddedIntegrationEvent);
        _logger.LogInformation("Published integration event: {EventName}", employeeAddedIntegrationEvent.GetType().Name);
    }

    private string GetRandomPassword()
    {
        var length = 8;
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        var password = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        return password;
    }
}