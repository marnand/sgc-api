using sgc.Domain.Entities;

namespace sgc.Domain.Dtos.Customer;

public record class UpdateCustomerDto(
    Guid Id,
    string Name,
    TypeCustomerEnum Type,
    DocumentTypeEnum DocumentType,
    string DocumentNumber,
    string Email,
    string Phone,
    DateTime? DeactivatedAt
) : CustomerDto(Name, Type, DocumentType, DocumentNumber, Email, Phone);