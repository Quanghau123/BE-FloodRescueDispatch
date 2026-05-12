using Core.Application.Interfaces.Persistence;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class ShelterRepository : IShelterRepository
{
    private readonly AppDbContext _dbContext;

    public ShelterRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Shelter?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Shelters.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(Shelter entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Shelters.AddAsync(entity, cancellationToken);
    }
}

