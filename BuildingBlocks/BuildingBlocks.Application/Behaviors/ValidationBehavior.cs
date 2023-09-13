using BuildingBlocks.Application.CQRS;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommandBase
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<IValidator<TRequest>> _logger;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators,
                              ILogger<IValidator<TRequest>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request,
                                  RequestHandlerDelegate<TResponse> next,
                                  CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return await next();
        
        var failures = _validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        if (failures.Any())
        {
            _logger.LogWarning("Validation errors - {RequestType} - Request: {@Request} - Errors: {@ValidationErrors}",
                typeof(TRequest).Name, request, failures);
            throw new ValidationException(failures);
        }

        return await next();
    }
}