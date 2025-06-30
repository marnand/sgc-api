using System;
using sgc.Domain.Dtos.Address;
using sgc.Domain.Entities.Handlers;

namespace sgc.Domain.Interfaces.Services;

public interface IAddressService
{
  Task<ResultData<bool>> Register(AddressDto dto);
}
