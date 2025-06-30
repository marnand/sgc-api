using System.Data;

namespace sgc.Domain.Interfaces.Repositories;

public interface IBaseRepository
{
    void BeginTransaction();
    void JoinTransaction(IDbTransaction transaction);
    void CommitTransaction();
    void RollbackTransaction();
    IDbTransaction? GetCurrentTransaction();
}
