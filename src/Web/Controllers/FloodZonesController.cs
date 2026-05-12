using AutoMapper;
using Core.Application.Commands.FloodZones;
using Core.Application.Queries.FloodZones;
using Core.Application.Services;
using Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Web.DTOs.Requests;
using Web.DTOs.Responses;

namespace Web.Controllers;

[ApiController]
[Route("api/flood-zones")]
public sealed class FloodZonesController : ControllerBase
{
    private readonly FloodZoneService _floodZoneService;
    private readonly IMapper _mapper;

    public FloodZonesController(FloodZoneService floodZoneService, IMapper mapper)
    {
        _floodZoneService = floodZoneService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<FloodZoneMapResponse>> Create(
        [FromBody] CreateFloodZoneRequest request,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateFloodZoneCommand>(request);
        var zone = await _floodZoneService.CreateAsync(command, cancellationToken);

        return Ok(_mapper.Map<FloodZoneMapResponse>(zone));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateFloodZoneRequest request,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<UpdateFloodZoneCommand>(request);
        command.Id = id;

        await _floodZoneService.UpdateAsync(command, cancellationToken);

        return NoContent();
    }

    [HttpGet("map")]
    public async Task<ActionResult<IReadOnlyList<FloodZoneMapResponse>>> GetMap(
        [FromQuery] double minLng,
        [FromQuery] double minLat,
        [FromQuery] double maxLng,
        [FromQuery] double maxLat,
        [FromQuery] int? zoom,
        [FromQuery] FloodSeverity? severity,
        [FromQuery] FloodZoneStatus? status,
        CancellationToken cancellationToken)
    {
        var items = await _floodZoneService.GetMapItemsAsync(new GetFloodZonesMapQuery
        {
            MinLng = minLng,
            MinLat = minLat,
            MaxLng = maxLng,
            MaxLat = maxLat,
            Zoom = zoom,
            Severity = severity,
            Status = status
        }, cancellationToken);

        return Ok(_mapper.Map<IReadOnlyList<FloodZoneMapResponse>>(items));
    }
}

