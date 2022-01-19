using Microsoft.EntityFrameworkCore;
using PublicForum.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum.Repository.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : class
    {
        protected readonly DbSet<T> _dbSet;
        public RepositoryBase(EFContext context)
        {
            _dbSet = context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }

        public async Task<List<T>> ListAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return await Task.FromResult(entity);
        }
    }
}
