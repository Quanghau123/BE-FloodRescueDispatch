namespace Web.DTOs.Responses;

public sealed class DashboardSummaryResponse
{
    public long PendingSosCount { get; set; }

    public long AssignedSosCount { get; set; }

    public long InProgressSosCount { get; set; }

    public long ResolvedSosCount { get; set; }

    public long AvailableTeamCount { get; set; }

    public long ActiveFloodZoneCount { get; set; }
}

