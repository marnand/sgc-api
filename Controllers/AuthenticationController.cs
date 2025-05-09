using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sgc.Domain.Dtos.User;
using sgc.Domain.Interfaces.Services;

namespace sgc.Controllers;

[ApiController]
[Route("v1/api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger<CollaboratorController> _logger;
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(ILogger<CollaboratorController> logger, 
        IAuthenticationService authenticationService)
    {
        _logger = logger;
        _authenticationService = authenticationService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequest req)
    {
        var result = await _authenticationService.Login(req);
        return result.IsMatch<IActionResult>(Ok, BadRequest);
    }
}
