using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sgc.Domain.Dtos.Address;
using sgc.Domain.Entities;
using sgc.Domain.Interfaces.Services;

namespace sgc.Controllers;

[ApiController]
[Route("v1/api/endereco")]
public class AddressController : ControllerBase
{
    private readonly IAddressService _addressService;

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpPost()]
    [Authorize(Roles = nameof(RoleEnum.Client))]
    public async Task<IActionResult> Register([FromBody] AddressDto request)
    {
        var result = await _addressService.Register(request);
        return result.IsMatch<IActionResult>(Ok, BadRequest);
    }
}
