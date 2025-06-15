using sgc.Domain.Dtos.BankDetails;
using sgc.Domain.Entities.Handlers;

namespace sgc.Domain.Entities;

public class BankDetails : Entity
{
    public Guid Id { get; private set; }
    public string Bank { get; private set; } = string.Empty;
    public string Agency { get; private set; } = string.Empty;
    public string Account { get; private set; } = string.Empty;
    private AccountTypeEnum _accountType;
    public AccountTypeEnum AccountType
    {
        get => _accountType;
        private set => _accountType = value;
    }
    public DateTime? DeactivatedAt { get; private set; }

    public ResultData<BankDetails> Create(BankDetailsDto dto)
    {
        Id = Guid.NewGuid();
        Bank = dto.Bank;
        Agency = dto.Agency;
        Account = dto.Account;
        AccountType = dto.AccountType;
        return ResultData<BankDetails>.Success(this);
    }
}

public enum AccountTypeEnum
{
    Corrente = 1,
    Poupanca
}