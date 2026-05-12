using Core.Application.Commands.Sos;
using Core.Application.Common.Paging;
using Core.Application.Interfaces.Notifications;
using Core.Application.Interfaces.Persistence;
using Core.Application.Queries.Sos;
using Core.Application.Strategies;
using Core.Domain.Entities;
using Core.Domain.Enums;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace Core.Application.Services;

public sealed class SosService
{
    private readonly ISosRepository _sosRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly PriorityScoreCalculator _priorityScoreCalculator;
    private readonly INotificationService _notificationService;
    private readonly GeometryFactory _geometryFactory;

    public SosService(
        ISosRepository sosRepository,
        IUnitOfWork unitOfWork,
        PriorityScoreCalculator priorityScoreCalculator,
        INotificationService notificationService)
    {
        _sosRepository = sosRepository;
        _unitOfWork = unitOfWork;
        _priorityScoreCalculator = priorityScoreCalculator;
        _notificationService = notificationService;
        _geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
    }

    public async Task<SosRequest> CreateAsync(CreateSosCommand command, CancellationToken cancellationToken = default)
    {
        var sos = new SosRequest
        {
            CitizenId = command.CitizenId,
            Location = _geometryFactory.CreatePoint(new Coordinate(command.Longitude, command.Latitude)),
            AddressText = command.AddressText,
            Description = command.Description,
            PeopleCount = command.PeopleCount,
            HasInjuredPeople = command.HasInjuredPeople,
            HasChildren = command.HasChildren,
            HasElderly = command.HasElderly,
            Status = SosStatus.Pending,
            PriorityScore = _priorityScoreCalculator.Calculate(command),
            CreatedAt = DateTimeOffset.UtcNow
        };

        await _sosRepository.AddAsync(sos, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _notificationService.NotifyCitizenSosCreatedAsync(sos, cancellationToken);
        await _notificationService.NotifyDispatchSosCreatedAsync(sos, cancellationToken);

        return sos;
    }

    public async Task CancelAsync(CancelSosCommand command, CancellationToken cancellationToken = default)
    {
        var sos = await _sosRepository.GetByIdAsync(command.SosRequestId, cancellationToken)
            ?? throw new InvalidOperationException("SOS request not found.");

        if (sos.CitizenId != command.CitizenId)
            throw new UnauthorizedAccessException("Cannot cancel another user's SOS.");

        if (sos.Status is SosStatus.Resolved or SosStatus.Cancelled)
            throw new InvalidOperationException("SOS cannot be cancelled.");

        sos.Status = SosStatus.Cancelled;
        sos.UpdatedAt = DateTimeOffset.UtcNow;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateStatusAsync(UpdateSosStatusCommand command, CancellationToken cancellationToken = default)
    {
        var sos = await _sosRepository.GetByIdAsync(command.SosRequestId, cancellationToken)
            ?? throw new InvalidOperationException("SOS request not found.");

        sos.Status = command.Status;
        sos.UpdatedAt = DateTimeOffset.UtcNow;

        if (command.Status == SosStatus.Resolved)
            sos.ResolvedAt = DateTimeOffset.UtcNow;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public Task<PagedResult<SosRequest>> SearchAsync(SearchSosQuery query, CancellationToken cancellationToken = default)
    {
        return _sosRepository.SearchAsync(query, cancellationToken);
    }

    public Task<IReadOnlyList<SosRequest>> GetMapItemsAsync(GetSosMapQuery query, CancellationToken cancellationToken = default)
    {
        query.Validate();
        return _sosRepository.GetMapItemsAsync(query, cancellationToken);
    }

    public Task<SosRequest?> GetDetailAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _sosRepository.GetDetailAsync(id, cancellationToken);
    }
}
