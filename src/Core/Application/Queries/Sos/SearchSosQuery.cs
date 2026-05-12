using Core.Application.Common.Paging;
using Core.Domain.Enums;

namespace Core.Application.Queries.Sos;

public sealed class SearchSosQuery : PagingQuery
{
    public SosStatus? Status { get; set; }

    public DateTimeOffset? CreatedFrom { get; set; }

    public DateTimeOffset? CreatedTo { get; set; }

    public string? SortBy { get; set; } = "priority_score";

    public string? SortDirection { get; set; } = "desc";
}

