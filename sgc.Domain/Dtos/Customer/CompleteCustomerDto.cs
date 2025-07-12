using sgc.Domain.Dtos.Address;
using sgc.Domain.Dtos.BankDetails;
using sgc.Domain.Entities;

namespace sgc.Domain.Dtos.Customer;

public class CompleteCustomerDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public TypeCustomerEnum Type { get; set; }
    public DocumentTypeEnum DocumentType { get; set; }
    public string DocumentNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public AddressDto? Address { get; set; }
    public BankDetailsDto? BankDetails { get; set; }
}
