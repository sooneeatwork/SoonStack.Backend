using DapperPersistance;
using System;
using System.Data;

namespace DapperPersistence
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IDbConnection _connection;
        private IDbTransaction? _transaction;
        private bool _disposed;

        public UnitOfWork(IDbConnection connection)
        {
            _connection = connection;
        }

        public IDbTransaction BeginTransaction()
        {
            // Check if the connection is closed and open it if necessary
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }

            _transaction = _connection.BeginTransaction();
            return _transaction;
        }

        public void CommitTransaction(IDbTransaction? dbTransaction)
        {
            dbTransaction?.Commit();
            dbTransaction?.Dispose();
            _transaction = null;
        }

        public void RollbackTransaction(IDbTransaction? dbTransaction)
        {
            dbTransaction?.Rollback();
            dbTransaction?.Dispose();
            _transaction = null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _transaction?.Dispose();
                    _connection?.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
