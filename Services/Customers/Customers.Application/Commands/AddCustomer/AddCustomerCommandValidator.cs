using Customers.Domain.Constants;
using FluentValidation;

namespace Customers.Application.Commands.AddCustomer;

public class AddCustomerCommandValidator : AbstractValidator<AddCustomerCommand>
{
    public AddCustomerCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .MaximumLength(StringLength.Name);

        RuleFor(command => command.ContactName)
            .MaximumLength(StringLength.Name)
            .When(x => !string.IsNullOrWhiteSpace(x.ContactName));
        
        RuleFor(command => command.Email)
            .MaximumLength(StringLength.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email));
        
        RuleFor(command => command.Address)
            .MaximumLength(StringLength.Address)
            .When(x => !string.IsNullOrWhiteSpace(x.Address));
        
        RuleFor(command => command.City)
            .MaximumLength(StringLength.City)
            .When(x => !string.IsNullOrWhiteSpace(x.City));
        
        RuleFor(command => command.State)
            .MaximumLength(StringLength.State)
            .When(x => !string.IsNullOrWhiteSpace(x.State));
        
        RuleFor(command => command.PostalCode)
            .MaximumLength(StringLength.PostalCode)
            .When(x => !string.IsNullOrWhiteSpace(x.PostalCode));
        
        RuleFor(command => command.Phone)
            .MaximumLength(StringLength.Phone)
            .Must(x => x.All(char.IsDigit))
            .When(x => !string.IsNullOrWhiteSpace(x.Phone));
    }
}