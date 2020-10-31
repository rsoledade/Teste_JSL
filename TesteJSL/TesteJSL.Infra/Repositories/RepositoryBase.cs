using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using TesteJSL.Domain.Interfaces;
using System.Collections.Generic;
using TesteJSL.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace TesteJSL.Infra.Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly MotoristaContext Context;

        public RepositoryBase(MotoristaContext motoristaContext) => Context = motoristaContext;

        public void BeginTransaction()
        {
            Context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            Context.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            Context.Database.RollbackTransaction();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var value = await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();
            return value.Entity;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var entity = await Context.Set<TEntity>().FindAsync(id);
            return entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                Context.Dispose();
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().AsNoTracking().Where(predicate).AnyAsync();
        }
    }
}
