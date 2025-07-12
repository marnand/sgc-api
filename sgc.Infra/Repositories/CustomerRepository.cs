using sgc.Domain.Dtos.Address;
using sgc.Domain.Dtos.BankDetails;
using sgc.Domain.Dtos.Customer;
using sgc.Domain.Entities;
using sgc.Domain.Entities.Handlers;
using sgc.Domain.Interfaces.Repositories;
using sgc.Infra.Repositories.Abstraction;
using sgc.Infra.Security;
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

    public async Task<ResultData<IEnumerable<CompleteCustomerDto>>> GetAllInfo()
    {
        try
        {
            const string sql = @"SELECT c.id, c.name, c.customer_type_id as Type, 
                    c.document_type_id as DocumentType, c.document_number as DocumentNumber, 
                    c.email, c.phone,
                    a.id, a.customer_id as customerId, a.street, a.establishment_number, a.complement, 
                    a.neighborhood, a.city, a.state, a.zip_code,
                    b.id, b.customer_id as customerId, b.bank, b.agency, b.account, b.account_type_id
                FROM customer c
                LEFT JOIN address a ON a.customer_id = c.id AND a.deactivated_at IS NULL
                LEFT JOIN bank_details b ON b.customer_id = c.id AND b.deactivated_at IS NULL
                WHERE c.deactivated_at IS NULL";

            var customers = await QueryAsync<Customer, AddressDto, BankDetailsDto, CompleteCustomerDto>(
                sql,
                (customer, address, bankDetails) =>
                {
                    return new CompleteCustomerDto
                    {
                        Id = customer.Id,
                        Name = customer.Name,
                        Type = customer.Type,
                        DocumentType = customer.DocumentType,
                        DocumentNumber = customer.DocumentNumber,
                        Email = customer.Email,
                        Phone = customer.Phone,
                        Address = address,
                        BankDetails = bankDetails
                    };
                }
            );
                
            return ResultData<IEnumerable<CompleteCustomerDto>>.Success(customers);
        }
        catch (Exception ex)
        {
            return ResultData<IEnumerable<CompleteCustomerDto>>.Failure(
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
