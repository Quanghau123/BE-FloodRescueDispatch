namespace Core.Application.Common.Paging;

public class PagedResult<T>
{
    public IReadOnlyCollection<T> Items { get; init; } = Array.Empty<T>();

    public int Page { get; init; }

    public int PageSize { get; init; }

    public long TotalItems { get; init; }

    public long TotalPages => PageSize <= 0 ? 0 : (long)Math.Ceiling(TotalItems / (double)PageSize);
}