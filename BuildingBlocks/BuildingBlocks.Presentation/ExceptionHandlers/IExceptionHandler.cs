using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Presentation.ExceptionHandlers;

public interface IExceptionHandler
{
    Task HandleAsync(HttpContext context, Exception exception);
}