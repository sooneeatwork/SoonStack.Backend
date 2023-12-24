using System.Data;

namespace EMS.Shared.Persistance
{
    public interface IUnitOfWork : IDisposable
    {
        IDbTransaction BeginTransaction();
        Task CommitAsync();
        void Rollback();

        Task<bool> SafeExecuteAsync(Task<bool> action);
    }
}