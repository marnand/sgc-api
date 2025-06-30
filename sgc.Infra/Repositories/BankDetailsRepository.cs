using System;
using sgc.Domain.Entities;
using sgc.Domain.Entities.Handlers;
using sgc.Domain.Interfaces.Repositories;
using sgc.Infra.Repositories.Abstraction;
using sgc.Infra.Security;
using System.Net;

namespace sgc.Infra.Repositories;

public class BankDetailsRepository(ConnectionFactory conn) : BaseRepository(conn), IBankDetailsRepository
{
    public async Task<ResultData<bool>> Create(BankDetails bankDetails)
    {
        try
        {
            const string sql = @"INSERT INTO bank_details (id, customer_id, bank, agency, account, account_type_id)
                VALUES (@Id, @CustomerId, @Bank, @Agency, @Account, @AccountType)";
            var result = await ExecuteAsync(sql, bankDetails);
            return ResultData<bool>.Success(result > 0);
        }
        catch (Exception ex)
        {
            return ResultData<bool>.Failure(
                $"Erro ao criar conta bancária: {ex.Message}",
                HttpStatusCode.InternalServerError);
        }
    }
}
