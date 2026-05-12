using Core.Application.Commands.Shelters;
using Core.Application.Interfaces.Persistence;
using Core.Domain.Entities;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace Core.Application.Services;

public sealed class ShelterService
{
    private readonly IShelterRepository _shelterRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly GeometryFactory _geometryFactory;

    public ShelterService(IShelterRepository shelterRepository, IUnitOfWork unitOfWork)
    {
        _shelterRepository = shelterRepository;
        _unitOfWork = unitOfWork;
        _geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
    }

    public async Task<Shelter> CreateAsync(CreateShelterCommand command, CancellationToken cancellationToken = default)
    {
        var shelter = new Shelter
        {
            Name = command.Name,
            Address = command.Address,
            Location = _geometryFactory.CreatePoint(new Coordinate(command.Longitude, command.Latitude)),
            Capacity = command.Capacity,
            ContactPhone = command.ContactPhone,
            HasMedicalSupport = command.HasMedicalSupport
        };

        await _shelterRepository.AddAsync(shelter, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return shelter;
    }

    public async Task UpdateAsync(UpdateShelterCommand command, CancellationToken cancellationToken = default)
    {
        var shelter = await _shelterRepository.GetByIdAsync(command.Id, cancellationToken)
            ?? throw new InvalidOperationException("Shelter not found.");

        shelter.Name = command.Name;
        shelter.Address = command.Address;
        shelter.Capacity = command.Capacity;
        shelter.CurrentOccupancy = command.CurrentOccupancy;
        shelter.Status = command.Status;
        shelter.ContactPhone = command.ContactPhone;
        shelter.HasMedicalSupport = command.HasMedicalSupport;
        shelter.UpdatedAt = DateTimeOffset.UtcNow;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
