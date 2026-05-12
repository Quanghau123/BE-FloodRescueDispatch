using Core.Domain.Entities;

namespace Core.Application.Interfaces.Persistence;

public interface IShelterRepository
{
    Task<Shelter?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task AddAsync(Shelter entity, CancellationToken cancellationToken = default);
}

