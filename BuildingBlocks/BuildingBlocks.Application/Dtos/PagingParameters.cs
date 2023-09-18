namespace BuildingBlocks.Application.Dtos;

public class PagingParameters
{
    private const int MaxPageSize = 1000;
    private const int DefaultPageSize = 10;
    private const int DefaultPageIndex = 1;
    private int _pageIndex = DefaultPageIndex;

    private int _pageSize = DefaultPageSize;

    public int PageIndex
    {
        get => _pageIndex;
        set => _pageIndex = value > 0 ? value : DefaultPageIndex;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value is >= 1 and <= MaxPageSize ? value : DefaultPageSize;
    }
    
    public string? Search { get; set; } = string.Empty;
}