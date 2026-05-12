using Core.Application.Common.Paging;
using Core.Application.Queries.Sos;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Persistence;

public interface ISosRepository
{
    Task<SosRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<SosRequest?> GetDetailAsync(Guid id, CancellationToken cancellationToken = default);

    Task AddAsync(SosRequest entity, CancellationToken cancellationToken = default);

    Task<PagedResult<SosRequest>> SearchAsync(SearchSosQuery query, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<SosRequest>> GetMapItemsAsync(GetSosMapQuery query, CancellationToken cancellationToken = default);
}

