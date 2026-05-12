namespace Web.DTOs.Responses;

public sealed class PagedResponse<T>
{
    public IReadOnlyList<T> Items { get; set; } = Array.Empty<T>();

    public int Page { get; set; }

    public int PageSize { get; set; }

    public long TotalItems { get; set; }

    public long TotalPages { get; set; }
}

