using Core.Application.Queries.FloodZones;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Persistence;

public interface IFloodZoneRepository
{
    Task<FloodZone?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task AddAsync(FloodZone entity, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<FloodZone>> GetMapItemsAsync(GetFloodZonesMapQuery query, CancellationToken cancellationToken = default);
}

