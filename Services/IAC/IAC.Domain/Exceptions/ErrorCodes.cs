namespace IAC.Domain.Exceptions;

public static class ErrorCodes
{
    public const string RoleNotFound = "Role:001";
    public const string RoleHasGrantedToUser = "Role:002";
    public const string DefaultRoleDeleteFailed = "Role:003";
    public const string UserAlreadyExist = "User:002";
    public const string UserCreateFail = "User:003";
    public const string UserNotFound = "User:001";
    public const string InvalidPassword = "User:002";
    public const string RefreshTokenNotFound = "RefreshToken:001";
    public const string RefreshTokenExpired = "RefreshToken:002";
}