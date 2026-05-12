using Core.Domain.Enums;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.DTOs.Responses;

namespace Web.Controllers;

[ApiController]
[Route("api/dashboard")]
public sealed class DashboardController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public DashboardController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("summary")]
    public async Task<ActionResult<DashboardSummaryResponse>> GetSummary(CancellationToken cancellationToken)
    {
        var response = new DashboardSummaryResponse
        {
            PendingSosCount = await _dbContext.SosRequests.AsNoTracking().LongCountAsync(x => x.Status == SosStatus.Pending, cancellationToken),
            AssignedSosCount = await _dbContext.SosRequests.AsNoTracking().LongCountAsync(x => x.Status == SosStatus.Assigned, cancellationToken),
            InProgressSosCount = await _dbContext.SosRequests.AsNoTracking().LongCountAsync(x => x.Status == SosStatus.InProgress, cancellationToken),
            ResolvedSosCount = await _dbContext.SosRequests.AsNoTracking().LongCountAsync(x => x.Status == SosStatus.Resolved, cancellationToken),
            AvailableTeamCount = await _dbContext.RescueTeams.AsNoTracking().LongCountAsync(x => x.Status == RescueTeamStatus.Available, cancellationToken),
            ActiveFloodZoneCount = await _dbContext.FloodZones.AsNoTracking().LongCountAsync(x => x.Status == FloodZoneStatus.Active, cancellationToken)
        };

        return Ok(response);
    }
}

