using Core.Application.Common;
using Core.Domain.Enums;

namespace Core.Application.Queries.FloodZones;

public sealed class GetFloodZonesMapQuery : BboxQuery
{
    public FloodSeverity? Severity { get; set; }

    public FloodZoneStatus? Status { get; set; }
}

