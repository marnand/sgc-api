using sgc.Domain.Dtos.Address;
using sgc.Domain.Dtos.BankDetails;

namespace sgc.Domain.Dtos.Customer;

public record class RegisterCustomerDto(CustomerDto Customer, AddressDto Address, BankDetailsDto? BankDetails);
