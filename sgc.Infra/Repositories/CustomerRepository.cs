using sgc.Domain.Entities;
using sgc.Domain.Entities.Handlers;
using sgc.Domain.Interfaces.Repositories;
using sgc.Infra.Repositories.Abstraction;
using sgc.Infra.Security;
using System.Data;
using System.Net;

namespace sgc.Infra.Repositories;

public class CustomerRepository(ConnectionFactory conn) : BaseRepository(conn), ICustomerRepository
{
    public async Task<ResultData<bool>> Create(Customer customer)
    {
        try
        {
            const string sql = @"INSERT INTO customer (id,name,customer_type_id,document_type_id,document_number,email,phone)
                VALUES (@Id,@Name,@Type,@DocumentType,@DocumentNumber,@Email,@Phone)";
            var result = await ExecuteAsync(sql, customer);
            return ResultData<bool>.Success(result > 0);
        }
        catch (Exception ex)
        {
            return ResultData<bool>.Failure(
                $"Erro ao criar colaborador: {ex.Message}",
                HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultData<IEnumerable<Customer>>> GetAll()
    {
        try
        {
            const string sql = @"SELECT id,name,customer_type_id as type,document_type_id as documentType,
                document_number as documentNumber,email,phone,created_at as createdAt,
                updated_at as updatedAt,deactivated_at as deactivatedAt FROM customer 
                WHERE deactivated_at IS NULL";

            var customers = await QueryAsync<Customer>(sql);

            return customers is not null
                ? ResultData<IEnumerable<Customer>>.Success(customers)
                : ResultData<IEnumerable<Customer>>.Failure("Colaborador não encontrado!", HttpStatusCode.NotFound);
        }
        catch (Exception ex)
        {
            return ResultData<IEnumerable<Customer>>.Failure(
                $"Erro ao buscar clientes: {ex.Message}",
                HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultData<Collaborator>> GetByEmail(string email)
    {
        try
        {
            const string sql = @"SELECT 
                id,username,name,email,role_id as roleId,avatar,created_at as createdAt,updated_at as updatedAt,deactivated_at as deactivatedAt
                FROM collaborator WHERE email = @Email";

            var user = await QueryFirstOrDefaultAsync<Collaborator>(sql, new { Email = email });
            return user is not null
                ? ResultData<Collaborator>.Success(user)
                : ResultData<Collaborator>.Failure("Colaborador não encontrado!", HttpStatusCode.NotFound);
        }
        catch (Exception ex)
        {
            return ResultData<Collaborator>.Failure(
                $"Erro ao buscar colaborador: {ex.Message}",
                HttpStatusCode.InternalServerError);
        }
    }
}
