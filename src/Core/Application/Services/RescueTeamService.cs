using Core.Application.Commands.RescueTeams;
using Core.Application.Interfaces.Persistence;
using Core.Domain.Entities;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace Core.Application.Services;

public sealed class RescueTeamService
{
    private readonly IRescueTeamRepository _rescueTeamRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly GeometryFactory _geometryFactory;

    public RescueTeamService(IRescueTeamRepository rescueTeamRepository, IUnitOfWork unitOfWork)
    {
        _rescueTeamRepository = rescueTeamRepository;
        _unitOfWork = unitOfWork;
        _geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
    }

    public async Task UpdateLocationAsync(UpdateRescueTeamLocationCommand command, CancellationToken cancellationToken = default)
    {
        var team = await _rescueTeamRepository.GetByIdAsync(command.RescueTeamId, cancellationToken)
            ?? throw new InvalidOperationException("Rescue team not found.");

        var location = _geometryFactory.CreatePoint(new Coordinate(command.Longitude, command.Latitude));

        team.CurrentLocation = location;
        team.LastLocationUpdatedAt = DateTimeOffset.UtcNow;
        team.UpdatedAt = DateTimeOffset.UtcNow;

        await _rescueTeamRepository.AddLocationHistoryAsync(new RescueTeamLocationHistory
        {
            RescueTeamId = team.Id,
            Location = location,
            CapturedAt = DateTimeOffset.UtcNow
        }, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateStatusAsync(UpdateRescueTeamStatusCommand command, CancellationToken cancellationToken = default)
    {
        var team = await _rescueTeamRepository.GetByIdAsync(command.RescueTeamId, cancellationToken)
            ?? throw new InvalidOperationException("Rescue team not found.");

        team.Status = command.Status;
        team.UpdatedAt = DateTimeOffset.UtcNow;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
