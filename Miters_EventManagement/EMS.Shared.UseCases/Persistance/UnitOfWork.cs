using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Shared.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;

        public UnitOfWork(IDbConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _connection.Open();  // Open the connection when UoW is created
        }

        public IDbTransaction BeginTransaction()
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }

            var transaction = _connection.BeginTransaction();  // Explicitly start the transaction

            return transaction;
        }

        public async Task CommitAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("No active transaction to commit.");
            }

            try
            {
                _transaction.Commit();
                await Task.CompletedTask;  // Placeholder for potential future async operations
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
                _connection.Close();  // Close the connection after committing or rolling back
            }
        }

        public void Rollback()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("No active transaction to roll back.");
            }

            try
            {
                _transaction.Rollback();
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
                _connection.Close();
            }
        }

        public void Dispose()
        {
            if (_transaction != null)  // If transaction is still active, rollback
            {
                Rollback();
            }
            _connection?.Dispose();
        }

        public async Task SafeExecuteAsync(Func<Task> action)
        {
            try
            {
                BeginTransaction();
                await action();
                await CommitAsync();
            }
            catch
            {
                Rollback();
                throw;
            }
        }

        public async Task<bool> SafeExecuteAsync(Task<bool> actionTask)
        {
            bool result = false;
            try
            {
                BeginTransaction();
                result = await actionTask;
                if (result)
                {
                    await CommitAsync();
                }
                else
                {
                    Rollback();
                }
            }
            catch
            {
                Rollback();
                throw;
            }
            return result;
        }

    }
}
