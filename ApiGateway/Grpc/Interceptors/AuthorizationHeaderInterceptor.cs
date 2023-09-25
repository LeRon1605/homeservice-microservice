using Grpc.Core;
using Grpc.Core.Interceptors;

namespace ApiGateway.Grpc.Interceptors;

public class AuthorizationHeaderInterceptor : Interceptor
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<AuthorizationHeaderInterceptor> _logger;

    public AuthorizationHeaderInterceptor(
        IHttpContextAccessor httpContextAccessor,
        ILogger<AuthorizationHeaderInterceptor> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public override TResponse BlockingUnaryCall<TRequest, TResponse>(
        TRequest request, 
        ClientInterceptorContext<TRequest, TResponse> context,
        BlockingUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        var hasAccessToken = _httpContextAccessor.HttpContext?.Request.Headers.ContainsKey("Authorization");

        if (hasAccessToken.HasValue && hasAccessToken.Value)
        {
            var accessToken = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"];

            var headers = new Metadata();
            headers.Add(new Metadata.Entry("Authorization", accessToken!));
            
            var newOptions = context.Options.WithHeaders(headers);
            var newContext = new ClientInterceptorContext<TRequest, TResponse>(
                context.Method,
                context.Host,
                newOptions);
            
            _logger.LogInformation("Making grpc call with authorization header!");
            
            return base.BlockingUnaryCall(request, newContext, continuation);
        }
     
        _logger.LogInformation("Making grpc call without authorization header!");
        return base.BlockingUnaryCall(request, context, continuation);
    }

    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
        TRequest request,
        ClientInterceptorContext<TRequest, TResponse> context, 
        Interceptor.AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        var hasAccessToken = _httpContextAccessor.HttpContext?.Request.Headers.ContainsKey("Authorization");

        if (hasAccessToken.HasValue && hasAccessToken.Value)
        {
            var accessToken = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"];

            var headers = new Metadata();
            headers.Add(new Metadata.Entry("Authorization", accessToken!));
            
            var newOptions = context.Options.WithHeaders(headers);
            var newContext = new ClientInterceptorContext<TRequest, TResponse>(
                context.Method,
                context.Host,
                newOptions);
            
            _logger.LogTrace("Making grpc call with authorization header!");
            
            return base.AsyncUnaryCall(request, newContext, continuation);
        }
     
        _logger.LogTrace("Making grpc call without authorization header!");
        return base.AsyncUnaryCall(request, context, continuation);
    }
}