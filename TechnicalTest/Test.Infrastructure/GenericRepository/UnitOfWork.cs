using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Interfaces;
using Test.Infrastructure.Data;

namespace Test.Infrastructure.GenericRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContextTechnicalTest _context;
        public UnitOfWork(DbContextTechnicalTest context)
        {_context = context;
            
        }
        public void Dispose() => _context.Dispose();

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            return new GenericRepository<T>(_context);
        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

    }
}
