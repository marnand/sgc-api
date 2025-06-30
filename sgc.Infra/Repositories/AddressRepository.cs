using System;
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
}
