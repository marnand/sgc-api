namespace sgc.Domain.Dtos.Address;

public record class AddressDto(
    Guid? CustomerId,
    string Street, 
    string EstablishmentNumber,
    string? Complement,
    string Neighborhood,
    string City,
    string State,
    string ZipCode
);
