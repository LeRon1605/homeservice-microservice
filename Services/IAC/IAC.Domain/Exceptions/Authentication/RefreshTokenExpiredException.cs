using BuildingBlocks.Domain.Exceptions;

namespace IAC.Domain.Exceptions.Authentication;

public class RefreshTokenExpiredException : CoreException
{
    public RefreshTokenExpiredException() : base("Refresh token expired", ErrorCodes.RefreshTokenExpired)
    {
    }
}