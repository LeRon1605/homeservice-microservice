﻿using System.Text;
using System.Text.Json;
using BuildingBlocks.Domain.Exceptions;
using BuildingBlocks.Domain.Exceptions.Resource;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Presentation.ExceptionHandlers;

public class ExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ExceptionHandler> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionHandler(ILogger<ExceptionHandler> logger, IHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public async Task HandleAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception.Message);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = GetResponseStatusCode(exception);

        var errorResponse = GetErrorResponse(exception);
        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }
    
    private ErrorResponse GetErrorResponse(Exception exception)
    {
        var response = new ErrorResponse();

        switch (exception)
        {
            case CoreException coreException:
                response.Code = coreException.ErrorCode;
                response.Message = coreException.Message;
                break;

            default:
                response.Code = ErrorCodes.SystemError;
                response.Message = _env.IsDevelopment() ? exception.Message : "Something went wrong, please try again!";
                break;
        }

        return response;
    }

    private static int GetResponseStatusCode(Exception exception)
    {
        switch (exception)
        {
            case ResourceNotFoundException:
                return StatusCodes.Status404NotFound;
            case ResourceAccessDeniedException:
                return StatusCodes.Status403Forbidden;
            case ResourceAlreadyExistException:
                return StatusCodes.Status409Conflict;
            case ResourceInvalidOperationException:
                return StatusCodes.Status400BadRequest;
            default:
                return StatusCodes.Status500InternalServerError;
        }

    }
}