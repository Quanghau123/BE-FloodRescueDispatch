namespace Core.Application.Queries.Dashboard;

public sealed class GetDashboardSummaryQuery
{
    public DateTimeOffset? From { get; set; }

    public DateTimeOffset? To { get; set; }
}

