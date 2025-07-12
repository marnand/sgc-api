using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sgc.Domain.Dtos.BankDetails;
using sgc.Domain.Entities;
using sgc.Domain.Interfaces.Services;

namespace sgc.Controllers;

[ApiController]
[Route("v1/api/conta-bancaria")]
public class BankDetailsController : ControllerBase
{
    private readonly IBankDetailsService _bankDetailsService;

    public BankDetailsController(IBankDetailsService bankDetailsService)
    {
        _bankDetailsService = bankDetailsService;
    }

    [HttpPost()]
    [Authorize(Roles = nameof(RoleEnum.Client))]
    public async Task<IActionResult> Register([FromBody] BankDetailsDto request)
    {
        var result = await _bankDetailsService.Register(request);
        return result.ToActionResult();
    }
}
