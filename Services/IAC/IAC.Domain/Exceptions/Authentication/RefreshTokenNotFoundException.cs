using BuildingBlocks.Domain.Exceptions.Resource;

namespace IAC.Domain.Exceptions.Authentication;

public class RefreshTokenNotFoundException : ResourceNotFoundException
{
    public RefreshTokenNotFoundException() : base("Refresh token not found!" ,ErrorCodes.RefreshTokenNotFound)
    {
    }
}