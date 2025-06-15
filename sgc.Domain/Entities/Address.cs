using sgc.Domain.Dtos.Address;
using sgc.Domain.Entities.Handlers;

namespace sgc.Domain.Entities;

public class Address : Entity
{
    public Guid Id { get; private set; }
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
}
