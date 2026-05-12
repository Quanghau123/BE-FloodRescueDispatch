using AutoMapper;
using Core.Application.Commands.RescueTeams;
using Core.Application.Interfaces.PostGIS;
using Core.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Web.DTOs.Requests;
using Web.DTOs.Responses;

namespace Web.Controllers;

[ApiController]
[Route("api/rescue-teams")]
public sealed class RescueTeamsController : ControllerBase
{
    private readonly RescueTeamService _rescueTeamService;
    private readonly IGeoQueryService _geoQueryService;
    private readonly IMapper _mapper;

    public RescueTeamsController(
        RescueTeamService rescueTeamService,
        IGeoQueryService geoQueryService,
        IMapper mapper)
    {
        _rescueTeamService = rescueTeamService;
        _geoQueryService = geoQueryService;
        _mapper = mapper;
    }

    [HttpPut("{id:guid}/location")]
    public async Task<IActionResult> UpdateLocation(
        Guid id,
        [FromBody] UpdateRescueTeamLocationRequest request,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<UpdateRescueTeamLocationCommand>(request);
        command.RescueTeamId = id;

        await _rescueTeamService.UpdateLocationAsync(command, cancellationToken);

        return NoContent();
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(
        Guid id,
        [FromBody] UpdateRescueTeamStatusRequest request,
        CancellationToken cancellationToken)
    {
        await _rescueTeamService.UpdateStatusAsync(new UpdateRescueTeamStatusCommand
        {
            RescueTeamId = id,
            Status = request.Status
        }, cancellationToken);

        return NoContent();
    }

    [HttpGet("nearest")]
    public async Task<ActionResult<NearestRescueTeamResponse>> FindNearest(
        [FromQuery] double longitude,
        [FromQuery] double latitude,
        [FromQuery] double radiusMeters = 20_000,
        CancellationToken cancellationToken = default)
    {
        var result = await _geoQueryService.FindNearestRescueTeamAsync(
            longitude,
            latitude,
            radiusMeters,
            cancellationToken);

        if (result == null)
            return NotFound();

        return Ok(_mapper.Map<NearestRescueTeamResponse>(result));
    }
}

