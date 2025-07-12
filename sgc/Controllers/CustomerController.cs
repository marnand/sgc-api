using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sgc.Domain.Dtos.Customer;
using sgc.Domain.Entities;
using sgc.Domain.Entities.Handlers;
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultData<bool>))]
    public async Task<IActionResult> Register([FromBody] CompleteCustomerDto request)
    {
        var result = await _customerService.Register(request);
        return result.ToActionResult();
    }

    [HttpGet()]
    [Authorize(Roles = nameof(RoleEnum.Client))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultData<CompleteCustomerDto>))]
    public async Task<IActionResult> GetAllInfo()
    {
        var result = await _customerService.GetAllInfo();
        return result.ToActionResult();
    }
}
