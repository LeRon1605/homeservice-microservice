namespace BuildingBlocks.Application.Cache;

public static class KeyGenerator
{
    public static string Generate(string prefix, string id)
    {
        return $"{prefix}:{id}";
    }
}