using sgc.Domain.Dtos.Customer;
using sgc.Domain.Entities;
using sgc.Domain.Entities.Handlers;
using sgc.Domain.Interfaces.Repositories;
using sgc.Domain.Interfaces.Services;
using System.Net;

namespace sgc.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IBankDetailsRepository _bankDatailsRepository;

    public CustomerService(
        ICustomerRepository customerRepository,
        IAddressRepository addressRepository,
        IBankDetailsRepository bankDatailsRepository
    )
    {
        _customerRepository = customerRepository;
        _addressRepository = addressRepository;
        _bankDatailsRepository = bankDatailsRepository;
    }

    public async Task<ResultData<bool>> Register(RegisterCustomerDto dto)
    {

        var resultCustomer = new Customer().Create(dto.Customer);
        if (!resultCustomer.IsSuccess)
            return ResultData<bool>.Failure(resultCustomer.Message, resultCustomer.StatusCode);

        var resultAddress = new Address().Create(dto.Address);
        if (!resultAddress.IsSuccess)
            return ResultData<bool>.Failure(resultAddress.Message, resultAddress.StatusCode);


        var resultBankDetails = ResultData<BankDetails>.Failure("", HttpStatusCode.BadRequest);
        if (dto.BankDetails is not null)
        {
            resultBankDetails = new BankDetails().Create(dto.BankDetails);
            if (!resultBankDetails.IsSuccess)
                return ResultData<bool>.Failure(resultBankDetails.Message, resultBankDetails.StatusCode);
        }

        _customerRepository.BeginTransaction();
        var transaction = _customerRepository.GetCurrentTransaction();

        _addressRepository.JoinTransaction(transaction!);
        _bankDatailsRepository.JoinTransaction(transaction!);

        var resultCustomerCreate = await _customerRepository.Create(resultCustomer.Data!);
        if (!resultCustomerCreate.IsSuccess)
        {
            _customerRepository.RollbackTransaction();
            return resultCustomerCreate;
        }

        resultAddress.Data!.SetCustomerId(resultCustomer.Data!.Id);
        var resultAddressCreate = await _addressRepository.Create(resultAddress.Data!);
        if (!resultAddressCreate.IsSuccess)
        {
            _customerRepository.RollbackTransaction();
            return resultAddressCreate;
        }

        if (dto.BankDetails is not null)
        {
            resultBankDetails.Data!.SetCustomerId(resultCustomer.Data!.Id);
            var resultBankDetailsCreate = await _bankDatailsRepository.Create(resultBankDetails.Data!);
            if (!resultBankDetailsCreate.IsSuccess)
            {
                _customerRepository.RollbackTransaction();
                return resultBankDetailsCreate;
            }
        }

        _customerRepository.CommitTransaction();
        return ResultData<bool>.Success(true);
    }

    public async Task<ResultData<IEnumerable<Customer>>> GetAll()
    {
        var result = await _customerRepository.GetAll();
        return result;
    }
}
