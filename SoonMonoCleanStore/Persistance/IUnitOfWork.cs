using System.Data;

namespace DapperPersistance
{
    public interface IUnitOfWork
    {
        IDbTransaction BeginTransaction();
        void CommitTransaction(IDbTransaction? dbTransaction);
        void RollbackTransaction(IDbTransaction? dbTransaction);
    }
}