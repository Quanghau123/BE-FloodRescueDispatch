using Core.Application.Interfaces.Persistence;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class RescueTeamRepository : IRescueTeamRepository
{
    private readonly AppDbContext _dbContext;

    public RescueTeamRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<RescueTeam?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.RescueTeams.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddLocationHistoryAsync(RescueTeamLocationHistory history, CancellationToken cancellationToken = default)
    {
        await _dbContext.RescueTeamLocationHistories.AddAsync(history, cancellationToken);
    }
}

