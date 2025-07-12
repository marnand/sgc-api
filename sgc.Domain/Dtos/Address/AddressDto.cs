namespace sgc.Domain.Dtos.Address;

public class AddressDto
{
    public Guid? Id { get; set; }
    public Guid? CustomerId { get; set; }
    public string Street { get; set; } = string.Empty;
    public string EstablishmentNumber { get; set; } = string.Empty;
    public string? Complement { get; set; } 
    public string Neighborhood { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
}

