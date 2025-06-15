using sgc.Domain.Entities;

namespace sgc.Domain.EntitiesValidations.Customer;

public record class CustomerValidationContext(
    string Name,
    TypeCustomerEnum Type, 
    DocumentTypeEnum DocumentType, 
    string DocumentNumber,
    string Email,
    string Phone
);
