using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sgc.Domain.Dtos.Collaborator;
using sgc.Domain.Entities;
using sgc.Domain.Entities.Handlers;
using sgc.Domain.Interfaces.Services;
using System.Net;

namespace sgc.Controllers;

[ApiController]
[Route("v1/api/colaborador")]
public class CollaboratorController : ControllerBase
{
    private readonly ILogger<CollaboratorController> _logger;
    private readonly ICollaboratorService _collaboratorService;

    public CollaboratorController(ILogger<CollaboratorController> logger, ICollaboratorService collaboratorService)
    {
        _logger = logger;
        _collaboratorService = collaboratorService;
    }

    [HttpPost()]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] CollaboratorDto request)
    {
        var result = await _collaboratorService.Register(request);
        return result.IsMatch<IActionResult>(Ok, BadRequest);
    }

    [HttpGet()]
    [Authorize(Roles = nameof(RoleEnum.Client))]
    public async Task<IActionResult> GetCurrency()
    {
        var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
        var token = authorizationHeader.Replace("Bearer ", "");

        var result = await _collaboratorService.GetByToken(token);
        return result.IsMatch(Ok, ErrorHandle);
    }

    private IActionResult ErrorHandle(ResultData<GetUserResponse> result)
    {
        if (result.StatusCode == HttpStatusCode.NotFound) return NotFound(result);
        if (result.StatusCode == HttpStatusCode.Unauthorized) return Unauthorized(result);

        return BadRequest(result);
    }
}
