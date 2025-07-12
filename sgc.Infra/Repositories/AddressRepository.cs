using sgc.Domain.Entities;
using sgc.Domain.Entities.Handlers;
using sgc.Domain.Interfaces.Repositories;
using sgc.Infra.Repositories.Abstraction;
using sgc.Infra.Security;
using System.Net;

namespace sgc.Infra.Repositories;

public class AddressRepository(ConnectionFactory conn) : BaseRepository(conn), IAddressRepository
{
    public async Task<ResultData<bool>> Create(Address address)
    {
        try
        {
            const string sql = @"INSERT INTO address (id,customer_id,street,establishment_number,complement,neighborhood,city,state,zip_code) 
                VALUES (@Id,@CustomerId,@Street,@EstablishmentNumber,@Complement,@Neighborhood,@City,@State,@ZipCode)";
            var result = await ExecuteAsync(sql, address);
            return ResultData<bool>.Success(result > 0);
        }
        catch (Exception ex)
        {
            return ResultData<bool>.Failure(
                $"Erro ao criar endereço: {ex.Message}",
                HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultData<Address?>> GetByCustomerId(Guid customerId)
    {
        try
        {
            const string sql = @"SELECT id,customer_id as customerId,street,establishment_number as establishmentNumber,
                complement,neighborhood,city,state,zip_code as zipCode FROM address WHERE customer_id = @customerId 
                AND deactivated_at IS NULL";
            var address = await QueryFirstOrDefaultAsync<Address>(sql, new { customerId });
            return ResultData<Address?>.Success(address);
        }
        catch (Exception ex)
        {
            return ResultData<Address?>.Failure(
                $"Erro ao buscar contas bancárias: {ex.Message}",
                HttpStatusCode.InternalServerError);
        }
    }
}
