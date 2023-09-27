namespace IAC.Application.Auth;

public class PermissionInfo
{
    public string Code { get; set; }
    public string Description { get; set; }

    public PermissionInfo(string code, string description)
    {
        Code = code;
        Description = description;
    }
}