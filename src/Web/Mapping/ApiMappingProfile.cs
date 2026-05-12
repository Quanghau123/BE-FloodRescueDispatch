using AutoMapper;
using Core.Application.Commands.Dispatch;
using Core.Application.Commands.FloodZones;
using Core.Application.Commands.RescueTeams;
using Core.Application.Commands.Shelters;
using Core.Application.Commands.Sos;
using Core.Application.Common.Paging;
using Core.Application.Interfaces.PostGIS;
using Core.Domain.Entities;
using NetTopologySuite.IO;
using Web.DTOs.Requests;
using Web.DTOs.Responses;

namespace Web.Mapping;

public sealed class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        CreateMap<CreateSosRequest, CreateSosCommand>();
        CreateMap<AssignRescueTeamRequest, AssignRescueTeamCommand>();
        CreateMap<UpdateRescueTeamLocationRequest, UpdateRescueTeamLocationCommand>();
        CreateMap<UpdateRescueTeamStatusRequest, UpdateRescueTeamStatusCommand>();
        CreateMap<CreateFloodZoneRequest, CreateFloodZoneCommand>();
        CreateMap<UpdateFloodZoneRequest, UpdateFloodZoneCommand>();
        CreateMap<CreateShelterRequest, CreateShelterCommand>();
        CreateMap<UpdateShelterRequest, UpdateShelterCommand>();

        CreateMap<SosRequest, SosSummaryResponse>()
            .ForMember(d => d.Longitude, o => o.MapFrom(s => s.Location.X))
            .ForMember(d => d.Latitude, o => o.MapFrom(s => s.Location.Y));

        CreateMap<SosRequest, SosMapItemResponse>()
            .ForMember(d => d.Longitude, o => o.MapFrom(s => s.Location.X))
            .ForMember(d => d.Latitude, o => o.MapFrom(s => s.Location.Y));

        CreateMap<SosRequest, SosDetailResponse>()
            .ForMember(d => d.Longitude, o => o.MapFrom(s => s.Location.X))
            .ForMember(d => d.Latitude, o => o.MapFrom(s => s.Location.Y))
            .ForMember(d => d.CitizenName, o => o.MapFrom(s => s.Citizen == null ? null : s.Citizen.FullName))
            .ForMember(d => d.CitizenPhone, o => o.MapFrom(s => s.Citizen == null ? null : s.Citizen.PhoneNumber));

        CreateMap<RescueAssignment, RescueAssignmentResponse>()
            .ForMember(d => d.RescueTeamName, o => o.MapFrom(s => s.RescueTeam == null ? null : s.RescueTeam.Name));

        CreateMap<FloodZone, FloodZoneMapResponse>()
            .ForMember(d => d.WktBoundary, o => o.MapFrom(s => new WKTWriter().Write(s.Boundary)));

        CreateMap<Shelter, ShelterResponse>()
            .ForMember(d => d.Longitude, o => o.MapFrom(s => s.Location.X))
            .ForMember(d => d.Latitude, o => o.MapFrom(s => s.Location.Y))
            .ForMember(d => d.AvailableSlots, o => o.MapFrom(s => s.AvailableSlots));

        CreateMap<RescueTeam, RescueTeamResponse>()
            .ForMember(d => d.Longitude, o => o.MapFrom(s => s.CurrentLocation == null ? null : (double?)s.CurrentLocation.X))
            .ForMember(d => d.Latitude, o => o.MapFrom(s => s.CurrentLocation == null ? null : (double?)s.CurrentLocation.Y));

        CreateMap<NearestShelterResult, NearestShelterResponse>();
        CreateMap<NearestRescueTeamResult, NearestRescueTeamResponse>();
        CreateMap<FloodAlertResult, AlertResponse>();

        CreateMap(typeof(PagedResult<>), typeof(PagedResponse<>));
    }
}

