using sgc.Domain.Dtos.Customer;
using sgc.Domain.Entities;
using sgc.Domain.Entities.Handlers;
using sgc.Domain.Interfaces.Repositories;
using sgc.Domain.Interfaces.Services;

namespace sgc.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<ResultData<bool>> Register(CustomerDto dto)
    {
        var result = new Customer().Create(dto);
        if (!result.IsSuccess)
            return ResultData<bool>.Failure(result.Message, result.StatusCode);

        var resultCreate = await _customerRepository.Create(result.Data!);
        return resultCreate;
    }

    public async Task<ResultData<IEnumerable<Customer>>> GetAll()
    {
        var result = await _customerRepository.GetAll();
        return result;
    }
}
