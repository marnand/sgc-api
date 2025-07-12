using System.Net;
using sgc.Domain.Dtos.BankDetails;
using sgc.Domain.Entities.Handlers;
using sgc.Domain.EntitiesValidations.BankDetails;

namespace sgc.Domain.Entities;

// TODO: Adicionar, na classe pai Entity, propriedade Ativo tipo boleano e adicionar condição na tabela para campo único:
//      - Propriedade Bank, Agency, Account deve ser unique, se o campo Ativo estiver true.

public class BankDetails : Entity
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
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
        var validationContext = new BankDetailsValidationContext(
            dto.Bank, dto.Agency, dto.Account, dto.AccountType
        );
        var validator = new BankDetailsValidator();
        var result = validator.Validate(validationContext);

        if (!result.IsValid)
            return ResultData<BankDetails>.Failure(result.ErrorMessages[0], HttpStatusCode.BadRequest);

        Id = Guid.NewGuid();
        Bank = dto.Bank;
        Agency = dto.Agency;
        Account = dto.Account;
        AccountType = dto.AccountType;
        return ResultData<BankDetails>.Success(this);
    }

    public void SetCustomerId(Guid customerId)
    {
        CustomerId = customerId;
    }
}

public enum AccountTypeEnum
{
    Corrente = 1,
    Poupanca
}