using System.Net;
using sgc.Domain.Dtos.Address;
using sgc.Domain.Entities.Handlers;
using sgc.Domain.EntitiesValidations.Address;

namespace sgc.Domain.Entities;

public class Address : Entity
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public string Street { get; private set; } = string.Empty;
    public string EstablishmentNumber { get; private set; } = string.Empty;
    public string? Complement { get; private set; } = string.Empty;
    public string Neighborhood { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string State { get; private set; } = string.Empty;
    public string ZipCode { get; private set; } = string.Empty;
    public DateTime? DeactivatedAt { get; private set; }

    public ResultData<Address> Create(AddressDto dto)
    {
        var validationContext = new AddressValidationContext(
            dto.Street, dto.EstablishmentNumber,dto.Neighborhood, dto.City, dto.State, dto.ZipCode
        );
        var validator = new AddressValidator();
        var result = validator.Validate(validationContext);

        if (!result.IsValid)
        {
            return ResultData<Address>.Failure(result.ErrorMessages[0], HttpStatusCode.BadRequest);
        }

        Id = Guid.NewGuid();
        Street = dto.Street;
        EstablishmentNumber = dto.EstablishmentNumber;
        Complement = dto.Complement;
        Neighborhood = dto.Neighborhood;
        City = dto.City;
        State = dto.State;
        ZipCode = dto.ZipCode;
        return ResultData<Address>.Success(this);
    }
    
    public void SetCustomerId(Guid customerId)
    {
        CustomerId = customerId;
    }
}
