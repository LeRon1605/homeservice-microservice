using System.Net;

namespace ApiGateway.Exceptions;

public class HttpClientException : Exception
{
    public HttpStatusCode StatusCode { get; set; }
    public HttpClientException(HttpStatusCode code, string? message = "") : base(message ?? $"Http client error with status code: {code}")
    {
        StatusCode = code;
    }
}