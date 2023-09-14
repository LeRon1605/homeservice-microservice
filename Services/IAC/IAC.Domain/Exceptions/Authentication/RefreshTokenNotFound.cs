using BuildingBlocks.Domain.Exceptions.Resource;

namespace IAC.Domain.Exceptions.Authentication;

public class RefreshTokenNotFound : ResourceNotFoundException
{
    public RefreshTokenNotFound() : base("Refresh token not found" ,ErrorCodes.RefreshTokenNotFound)
    {
    }
}