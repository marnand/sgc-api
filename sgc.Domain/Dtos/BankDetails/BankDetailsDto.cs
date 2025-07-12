using sgc.Domain.Entities;

namespace sgc.Domain.Dtos.BankDetails;

public class BankDetailsDto
{
    public Guid? Id { get; set; }
    public Guid? CustomerId { get; set; }
    public string Bank { get; set; } = string.Empty;
    public string Agency { get; set; } = string.Empty;
    public string Account { get; set; } = string.Empty;
    public AccountTypeEnum AccountType { get; set; }
}
