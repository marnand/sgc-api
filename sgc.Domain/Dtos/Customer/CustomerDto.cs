using sgc.Domain.Entities;

namespace sgc.Domain.Dtos.Customer;

public record class CustomerDto(
    string Name, 
    TypeCustomerEnum Type, 
    DocumentTypeEnum DocumentType, 
    string DocumentNumber,
    string Email,
    string Phone,
    DateTime? DeactivatedAt
);
