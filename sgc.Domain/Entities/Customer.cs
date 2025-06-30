using sgc.Domain.Dtos.Customer;
using sgc.Domain.Entities.Handlers;
using sgc.Domain.EntitiesValidations.Customer;
using System.Net;

namespace sgc.Domain.Entities;

public class Customer : Entity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    private TypeCustomerEnum _type;
    public TypeCustomerEnum Type
    {
        get => _type;
        private set => _type = value;
    }
    private DocumentTypeEnum _documentType;
    public DocumentTypeEnum DocumentType
    {
        get => _documentType;
        private set => _documentType = value;
    }
    public string DocumentNumber { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;
    public DateTime? DeactivatedAt { get; private set; }

    public ResultData<Customer> Create(CustomerDto dto)
    {
        var validationContext = new CustomerValidationContext(
            dto.Name, dto.Type, dto.DocumentType, dto.DocumentNumber, dto.Email, dto.Phone
        );
        var validator = new CustomerValidator();
        var result = validator.Validate(validationContext);

        if (!result.IsValid)
        {
            return ResultData<Customer>.Failure(result.ErrorMessages[0], HttpStatusCode.BadRequest);
        }

        Id = Guid.NewGuid();
        Name = dto.Name;
        Type = dto.Type;
        DocumentType = dto.DocumentType;
        DocumentNumber = dto.DocumentNumber;
        Email = dto.Email;
        Phone = dto.Phone;

        return ResultData<Customer>.Success(this);
    }
}

public enum TypeCustomerEnum
{
    Fisica = 1,
    Juridica
}

public enum DocumentTypeEnum
{
    CPF = 1,
    CNPJ
}