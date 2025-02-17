using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Interfaces;
using Test.Infrastructure.Data;

namespace Test.Infrastructure.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContextTechnicalTest _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(DbContextTechnicalTest context)
        {
            _context = context;
            _dbSet=context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);


        public void Delete(T entity) => _dbSet.Remove(entity);


        public async Task<T> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public Task<T1> GetByIdAsync<T1>(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity) => _dbSet.Update(entity);

       
    }
}
