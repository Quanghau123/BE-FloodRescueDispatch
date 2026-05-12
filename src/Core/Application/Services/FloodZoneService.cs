using Core.Application.Commands.FloodZones;
using Core.Application.Interfaces.Persistence;
using Core.Application.Queries.FloodZones;
using Core.Domain.Entities;
using NetTopologySuite.IO;

namespace Core.Application.Services;

public sealed class FloodZoneService
{
    private readonly IFloodZoneRepository _floodZoneRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly WKTReader _wktReader = new();

    public FloodZoneService(IFloodZoneRepository floodZoneRepository, IUnitOfWork unitOfWork)
    {
        _floodZoneRepository = floodZoneRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<FloodZone> CreateAsync(CreateFloodZoneCommand command, CancellationToken cancellationToken = default)
    {
        var polygon = (NetTopologySuite.Geometries.Polygon)_wktReader.Read(command.WktPolygon);
        polygon.SRID = 4326;

        var zone = new FloodZone
        {
            Name = command.Name,
            Severity = command.Severity,
            Boundary = polygon,
            Description = command.Description
        };

        await _floodZoneRepository.AddAsync(zone, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return zone;
    }

    public async Task UpdateAsync(UpdateFloodZoneCommand command, CancellationToken cancellationToken = default)
    {
        var zone = await _floodZoneRepository.GetByIdAsync(command.Id, cancellationToken)
            ?? throw new InvalidOperationException("Flood zone not found.");

        zone.Name = command.Name;
        zone.Severity = command.Severity;
        zone.Status = command.Status;
        zone.Description = command.Description;
        zone.UpdatedAt = DateTimeOffset.UtcNow;

        if (!string.IsNullOrWhiteSpace(command.WktPolygon))
        {
            var polygon = (NetTopologySuite.Geometries.Polygon)_wktReader.Read(command.WktPolygon);
            polygon.SRID = 4326;
            zone.Boundary = polygon;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public Task<IReadOnlyList<FloodZone>> GetMapItemsAsync(GetFloodZonesMapQuery query, CancellationToken cancellationToken = default)
    {
        query.Validate();
        return _floodZoneRepository.GetMapItemsAsync(query, cancellationToken);
    }
}

