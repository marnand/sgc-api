using System;
using sgc.Domain.Dtos.Address;
using sgc.Domain.Entities;
using sgc.Domain.Entities.Handlers;
using sgc.Domain.Interfaces.Repositories;
using sgc.Domain.Interfaces.Services;

namespace sgc.Application.Services;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;

    public AddressService(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }


    public async Task<ResultData<bool>> Register(AddressDto dto)
    {
        var result = new Address().Create(dto);
        if (!result.IsSuccess)
            return ResultData<bool>.Failure(result.Message, result.StatusCode);

        var resultCreate = await _addressRepository.Create(result.Data!);
        return resultCreate.IsSuccess
          ? ResultData<bool>.Success(resultCreate.Data)
          : ResultData<bool>.Failure(resultCreate.Message, resultCreate.StatusCode);
    }
}
