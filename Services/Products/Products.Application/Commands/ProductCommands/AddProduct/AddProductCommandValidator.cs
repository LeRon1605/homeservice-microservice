using FluentValidation;
using Products.Domain.Constant;

namespace Products.Application.Commands.ProductCommands.AddProduct;

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(StringLength.Name);
    }
}