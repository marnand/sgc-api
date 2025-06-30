using System;
using sgc.Domain.Dtos.BankDetails;
using sgc.Domain.Entities;
using sgc.Domain.Entities.Handlers;
using sgc.Domain.Interfaces.Repositories;
using sgc.Domain.Interfaces.Services;

namespace sgc.Application.Services;

public class BankDetailsService : IBankDetailsService
{
    private readonly IBankDetailsRepository _bankDetailsRepository;

    public BankDetailsService(IBankDetailsRepository bankDetailsRepository)
    {
        _bankDetailsRepository = bankDetailsRepository;
    }

    public async Task<ResultData<BankDetailsDto>> Register(BankDetailsDto dto)
    {
        var result = new BankDetails().Create(dto);
        if (!result.IsSuccess)
            return ResultData<BankDetailsDto>.Failure(result.Message, result.StatusCode);

        var resultCreate = await _bankDetailsRepository.Create(result.Data!);
        return resultCreate.IsSuccess 
            ? ResultData<BankDetailsDto>.Success(dto)
            : ResultData<BankDetailsDto>.Failure(resultCreate.Message, resultCreate.StatusCode);
    }
}
