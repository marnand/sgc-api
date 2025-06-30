using System;

namespace sgc.Domain.EntitiesValidations.Address;

public record class AddressValidationContext(
    string Street,
    string EstablishmentNumber,
    string Neighborhood,
    string City,
    string State,
    string ZipCode
);