using sgc.Domain.Entities;
using sgc.Domain.Entities.Handlers;
using sgc.Domain.Interfaces.Repositories;
using sgc.Infra.Repositories.Abstraction;
using sgc.Infra.Security;
using System.Net;

namespace sgc.Infra.Repositories;

public class CollaboratorRepository(ConnectionFactory conn) : BaseRepository(conn), ICollaboratorRepository
{
    public async Task<ResultData<bool>> Create(Collaborator collaborator)
    {
        try
        {
            const string sql = @"
                INSERT INTO collaborator (id,username,password,name,email,role_id,avatar,created_at)
                VALUES (@Id,@Username,@Password,@Name,@Email,@RoleId,@Avatar,@CreatedAt)";
            var result = await ExecuteAsync(sql, collaborator);
            return ResultData<bool>.Success(result > 0);
        }
        catch (Exception ex)
        {
            return ResultData<bool>.Failure(
                $"Erro ao criar colaborador: {ex.Message}",
                HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultData<Collaborator>> GetByUsername(string username)
    {
        try
        {
            const string sql = @"
                SELECT id,username,password,name,email,role_id as roleId,avatar,created_at as createdAt,
                updated_at as updatedAt,deactivated_at as deactivatedAt FROM collaborator 
                WHERE username = @username";

            var user = await QueryFirstOrDefaultAsync<Collaborator>(sql, new { username });

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
