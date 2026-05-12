using AutoMapper;
using Core.Application.Commands.Shelters;
using Core.Application.Interfaces.PostGIS;
using Core.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Web.DTOs.Requests;
using Web.DTOs.Responses;

namespace Web.Controllers;

[ApiController]
[Route("api/shelters")]
public sealed class SheltersController : ControllerBase
{
    private readonly ShelterService _shelterService;
    private readonly IGeoQueryService _geoQueryService;
    private readonly IMapper _mapper;

    public SheltersController(
        ShelterService shelterService,
        IGeoQueryService geoQueryService,
        IMapper mapper)
    {
        _shelterService = shelterService;
        _geoQueryService = geoQueryService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<ShelterResponse>> Create(
        [FromBody] CreateShelterRequest request,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateShelterCommand>(request);
        var shelter = await _shelterService.CreateAsync(command, cancellationToken);

        return Ok(_mapper.Map<ShelterResponse>(shelter));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateShelterRequest request,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<UpdateShelterCommand>(request);
        command.Id = id;

        await _shelterService.UpdateAsync(command, cancellationToken);

        return NoContent();
    }

    [HttpGet("nearest")]
    public async Task<ActionResult<NearestShelterResponse>> FindNearest(
        [FromQuery] double longitude,
        [FromQuery] double latitude,
        [FromQuery] double radiusMeters = 10_000,
        CancellationToken cancellationToken = default)
    {
        var result = await _geoQueryService.FindNearestShelterAsync(
            longitude,
            latitude,
            radiusMeters,
            cancellationToken);

        if (result == null)
            return NotFound();

        return Ok(_mapper.Map<NearestShelterResponse>(result));
    }
}

