namespace Core.Application.Common.Paging;

public class PagingQuery
{
    private const int MaxPageSize = 100;

    public int Page { get; set; } = 1;

    private int _pageSize = 20;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = Math.Clamp(value, 1, MaxPageSize);
    }

    public int Skip => (Page - 1) * PageSize;
}
