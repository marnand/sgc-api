using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sgc.Domain.Dtos.Customer;
using sgc.Domain.Entities;
using sgc.Domain.Interfaces.Services;

namespace sgc.Controllers;

[ApiController]
[Route("v1/api/cliente")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpPost()]
    [Authorize(Roles = nameof(RoleEnum.Client))]
    public async Task<IActionResult> Register([FromBody] CustomerDto request)
    {
        var result = await _customerService.Register(request);
        return result.IsMatch<IActionResult>(Ok, BadRequest);
    }
}
