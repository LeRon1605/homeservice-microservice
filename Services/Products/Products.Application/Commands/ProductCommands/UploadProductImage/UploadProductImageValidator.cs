using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Products.Application.Commands.ProductCommands.UploadProductImage;

public class UploadProductImageValidator : AbstractValidator<UploadProductImageCommand>
{
    private const int MaxSizeInMb = 20;

    public UploadProductImageValidator()
    {
        RuleFor(x => x.File)
            .NotNull()
            .Must(HaveValidContentType)
            .WithMessage("Invalid content type, file must be an image!")
            .Must(HaveValidSize)
            .WithMessage($"File size must be less than {MaxSizeInMb}MB!");
    }

    private static bool HaveValidContentType(IFormFile form)
    {
        if (!string.Equals(form.ContentType, "image/jpg", StringComparison.OrdinalIgnoreCase) &&
            !string.Equals(form.ContentType, "image/jpeg", StringComparison.OrdinalIgnoreCase) &&
            !string.Equals(form.ContentType, "image/pjpeg", StringComparison.OrdinalIgnoreCase) &&
            !string.Equals(form.ContentType, "image/gif", StringComparison.OrdinalIgnoreCase) &&
            !string.Equals(form.ContentType, "image/x-png", StringComparison.OrdinalIgnoreCase) &&
            !string.Equals(form.ContentType, "image/png", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }
        
        var postedFileExtension = Path.GetExtension(form.FileName);
        if (!string.Equals(postedFileExtension , ".jpg", StringComparison.OrdinalIgnoreCase)
            && !string.Equals(postedFileExtension , ".png", StringComparison.OrdinalIgnoreCase)
            && !string.Equals(postedFileExtension , ".gif", StringComparison.OrdinalIgnoreCase)
            && !string.Equals(postedFileExtension , ".jpeg", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        return true;
    }
    
    private bool HaveValidSize(IFormFile form)
    {
        return form.Length <= MaxSizeInMb * 1024 * 1024;
    }
}