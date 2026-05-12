using Core.Domain.Entities;

namespace Core.Application.Interfaces.Persistence;

public interface IRescueTeamRepository
{
    Task<RescueTeam?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task AddLocationHistoryAsync(RescueTeamLocationHistory history, CancellationToken cancellationToken = default);
}

