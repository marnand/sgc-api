using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sgc.Domain.Dtos.User;
using sgc.Domain.Entities.Handlers;
using sgc.Domain.Interfaces.Services;
using System.Net;
using LoginRequest = sgc.Domain.Dtos.User.LoginRequest;

namespace sgc.Controllers;

[ApiController]
[Route("v1/api/user")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet()]
    [Authorize]
    public async Task<IActionResult> GetCurrency()
    {
        var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
        var token = authorizationHeader.Replace("Bearer ", "");

        var result = await _userService.GetByToken(token);
        return result.IsMatch(Ok, ErrorHandle);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequest req)
    {
        var result = await _userService.Login(req);
        return result.IsMatch<IActionResult>(Ok, BadRequest);
    }

    private IActionResult ErrorHandle(ResultData<GetUserResponse> result)
    {
        if (result.StatusCode == HttpStatusCode.NotFound) return NotFound(result);
        if (result.StatusCode == HttpStatusCode.Unauthorized) return Unauthorized(result);

        return BadRequest(result);
    }
}
