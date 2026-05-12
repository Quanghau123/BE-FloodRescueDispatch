using AutoMapper;
using Core.Application.Commands.Dispatch;
using Core.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Web.DTOs.Requests;

namespace Web.Controllers;

[ApiController]
[Route("api/dispatch")]
public sealed class DispatchController : ControllerBase
{
    private readonly DispatchService _dispatchService;
    private readonly IMapper _mapper;

    public DispatchController(DispatchService dispatchService, IMapper mapper)
    {
        _dispatchService = dispatchService;
        _mapper = mapper;
    }

    [HttpPost("sos/{sosRequestId:guid}/assign")]
    public async Task<IActionResult> Assign(
        Guid sosRequestId,
        [FromBody] AssignRescueTeamRequest request,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<AssignRescueTeamCommand>(request);
        command.SosRequestId = sosRequestId;

        await _dispatchService.AssignAsync(command, cancellationToken);

        return NoContent();
    }
}

