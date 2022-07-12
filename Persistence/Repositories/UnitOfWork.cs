using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
 
using Application.Interfaces;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly MigrationContext _context;
        private bool disposed;
        public UnitOfWork(MigrationContext context)
        {
            _context = context;
        }

        public async Task<IDisposable> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default)
        {
            return await _context.Database.BeginTransactionAsync(isolationLevel, cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
            await _context.Database.CommitTransactionAsync(cancellationToken);
        }

        public async Task Rollback()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            // TODO: After dispose garbage Collector must clean up memory to free resources
            GC.SuppressFinalize(this);
        }

        // TODO : Dispose Pattern 
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //dispose managed resources
                    _context.Dispose();
                }
            }
            //dispose unmanaged resources
            disposed = true;
        }


    }
}
